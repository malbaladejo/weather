using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using WeatherApp.Extensions;

namespace WeatherApp.Services.Impl.Services.MeteoFrance
{
    internal class MeteoFranceFileReaderGeneric<T> : IMeteoFranceFileReaderGeneric<T>
    {
        private readonly IStationDataFactory<T> stationDataFactory;
        private readonly ILogger<MeteoFranceFileReaderGeneric<T>> logger;
        private readonly string dataPath;

        public MeteoFranceFileReaderGeneric(
            IWebHostEnvironment environment,
            ILogger<MeteoFranceFileReaderGeneric<T>> logger,
            IStationDataFactory<T> stationDataFactory)
        {
            dataPath = environment.WebRootPath;
            this.stationDataFactory = stationDataFactory;
            this.logger = logger;
        }

        public IEnumerable<T> ParseCsv(string csv)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var data = ParseCsvLines(csv.Split(Environment.NewLine));

            stopwatch.Stop();
            logger.LogInformation($"Csv loaded in {stopwatch.ElapsedMilliseconds}ms.");

            foreach (var item in data)
                yield return item;
        }

        private IEnumerable<T> ParseFile(string inputFile)
        {
            if (!File.Exists(inputFile))
            {
                logger.LogInformation($"The file {inputFile} does not exist.");
                yield break;
            }

            logger.LogInformation($"Load file {inputFile}.");
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            var csv = inputFile.ReadAllLines();

            var data = ParseCsvLines(csv);

            foreach (var item in data)
                yield return item;
        }

        private IEnumerable<T> ParseCsvLines(string[] lines)
        {
            logger.LogInformation($"csv contains {lines.Length} lines.");

            lines = lines.Select(l => l.Trim()).Where(l => !string.IsNullOrWhiteSpace(l)).ToArray();
            logger.LogInformation($"csv contains {lines.Length} not empty lines.");

            var stationDailyData = new List<T>();
            logger.LogInformation($"Parse csv");

            var headers = GetHeaders(lines[0]);

            for (int i = 1; i < lines.Length; i++)
            {
                if (string.IsNullOrEmpty(lines[i]))
                    continue;

                var data = lines[i].Split(";");
                try
                {
                    stationDailyData.Add(this.stationDataFactory.Create(data, headers));
                }
                catch (Exception e)
                {
                    logger.LogError($"Error line {i}");
                    logger.LogError(e.Message);
                }
            }

            logger.LogInformation($"Parse csv done");
            return stationDailyData;
        }


        private IReadOnlyDictionary<string, int> GetHeaders(string header)
        {
            var data = header.Split(";");
            var headers = new Dictionary<string, int>();

            for (int i = 0; i < data.Length; i++)
                headers[data[i]] = i;

            return headers;
        }
    }
}
