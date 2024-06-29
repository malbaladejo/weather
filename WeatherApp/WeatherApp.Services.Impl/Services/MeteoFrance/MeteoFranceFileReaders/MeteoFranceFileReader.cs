using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using WeatherApp.Extensions;
using WeatherApp.Models;

namespace WeatherApp.Services.Impl.Services.MeteoFrance
{
    [Obsolete("use HourMeteoFranceFileReader")]
    internal class MeteoFranceFileReader : IWeatherFileReader, IMeteoFranceFileReader
    {
        private readonly ILogger<MeteoFranceFileReader> logger;
        private readonly string dataPath;

        public MeteoFranceFileReader(IWebHostEnvironment environment, ILogger<MeteoFranceFileReader> logger)
        {
            dataPath = environment.WebRootPath;
            this.logger = logger;
        }

        public IEnumerable<WeatherData> Parse(int year, int month)
        {
            var inputFile = Path.Combine(dataPath, CercierConfig.FilePath(year, month));
            return ParseFile(inputFile).Select(x => x.ConvertToWeatherData());
        }

        public IEnumerable<HourStationData> ParseCsv(string csv)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var data = ParseCsvLines(csv.Split(Environment.NewLine));

            stopwatch.Stop();
            logger.LogInformation($"Csv loaded in {stopwatch.ElapsedMilliseconds}ms.");

            foreach (var item in data)
                yield return item;
        }

        public Task<IReadOnlyCollection<HourStationData>> GetLastDataAsync()
        {
            var cercierFolderPath = Path.Combine(dataPath, CercierConfig.FolderPath);

            var lastFile = Directory.GetFiles(cercierFolderPath)
                                    .OrderByDescending(p => p)
                                    .FirstOrDefault();

            if (lastFile == null)
                return Task.FromResult((IReadOnlyCollection<HourStationData>)new HourStationData[0]);

            return Task.FromResult<IReadOnlyCollection<HourStationData>>(ParseFile(lastFile).ToArray());
        }

        private IEnumerable<HourStationData> ParseFile(string inputFile)
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

        private IEnumerable<HourStationData> ParseCsvLines(string[] lines)
        {
            logger.LogInformation($"csv contains {lines.Length} lines.");

            lines = lines.Select(l => l.Trim()).Where(l => !string.IsNullOrWhiteSpace(l)).ToArray();
            logger.LogInformation($"csv contains {lines.Length} not empty lines.");

            var stationDailyData = new List<HourStationData>();
            logger.LogInformation($"Parse csv");

            var headers = GetHeaders(lines[0]);

            for (int i = 1; i < lines.Length; i++)
            {
                if (string.IsNullOrEmpty(lines[i]))
                    continue;

                var data = lines[i].Split(";");
                try
                {
                    stationDailyData.Add(new HourStationData
                    {
                        Post = data.GetValue<HourStationData>(headers, nameof(HourStationData.Post)),
                        Date = data.GetDate<HourStationData>(headers, nameof(HourStationData.Date)),
                        RainInMm = data.GetDecimal<HourStationData>(headers, nameof(HourStationData.RainInMm)),
                        Temperature = data.GetDecimal<HourStationData>(headers, nameof(HourStationData.Temperature)),
                        Pressure = data.GetDecimal<HourStationData>(headers, nameof(HourStationData.Pressure))
                    });
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
