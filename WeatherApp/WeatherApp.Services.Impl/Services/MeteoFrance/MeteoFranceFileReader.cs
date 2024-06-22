using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using WeatherApp.Extensions;
using WeatherApp.Models;

namespace WeatherApp.Services
{
    internal class MeteoFranceFileReader : IWeatherFileReader, IMeteoFranceFileReader
    {
        private readonly ILogger<MeteoFranceFileReader> logger;
        private readonly string dataPath;

        public MeteoFranceFileReader(IWebHostEnvironment environment, ILogger<MeteoFranceFileReader> logger)
        {
            this.dataPath = environment.WebRootPath;
            this.logger = logger;
        }

        public IEnumerable<WeatherData> Parse(int year, int month)
        {
            var inputFile = Path.Combine(this.dataPath, CercierConfig.FilePath(year, month));
            return this.ParseFile(inputFile).Select(x => x.ConvertToWeatherData());
        }

        public IEnumerable<HourStationData> ParseCsv(string csv)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var data = this.ParseCsvLines(csv.Split(Environment.NewLine));

            stopwatch.Stop();
            this.logger.LogInformation($"Csv loaded in {stopwatch.ElapsedMilliseconds}ms.");

            foreach (var item in data)
                yield return item;
        }

        public Task<IReadOnlyCollection<HourStationData>> GetLastDataAsync()
        {
            var cercierFolderPath = Path.Combine(this.dataPath, CercierConfig.FolderPath);

            var lastFile = Directory.GetFiles(cercierFolderPath)
                                    .OrderByDescending(p => p)
                                    .FirstOrDefault();

            if (lastFile == null)
                return Task.FromResult((IReadOnlyCollection<HourStationData>)new HourStationData[0]);

            return Task.FromResult<IReadOnlyCollection<HourStationData>>(this.ParseFile(lastFile).ToArray());
        }

        private IEnumerable<HourStationData> ParseFile(string inputFile)
        {
            if (!File.Exists(inputFile))
            {
                this.logger.LogInformation($"The file {inputFile} does not exist.");
                yield break;
            }

            this.logger.LogInformation($"Load file {inputFile}.");
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            var csv = inputFile.ReadAllLines();

            var data = this.ParseCsvLines(csv);

            foreach (var item in data)
                yield return item;
        }

        private IEnumerable<HourStationData> ParseCsvLines(string[] lines)
        {
            this.logger.LogInformation($"csv contains {lines.Length} lines.");

            lines = lines.Select(l => l.Trim()).Where(l => !string.IsNullOrWhiteSpace(l)).ToArray();
            this.logger.LogInformation($"csv contains {lines.Length} not empty lines.");

            var stationDailyData = new List<HourStationData>();
            this.logger.LogInformation($"Parse csv");

            var headers = this.GetHeaders(lines[0]);

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
                    this.logger.LogError($"Error line {i}");
                    this.logger.LogError(e.Message);
                }
            }

            this.logger.LogInformation($"Parse csv done");
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
