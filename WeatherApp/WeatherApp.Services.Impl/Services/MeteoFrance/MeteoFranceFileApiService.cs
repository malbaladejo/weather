using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using WeatherApp.Services.Impl.Services.MeteoFrance;

namespace WeatherApp.Services
{
    internal class MeteoFranceFileApiService : IMeteoFranceFileApiService
    {
        private readonly IMeteoFranceLiveApiService meteoFranceLiveApiService;
        private readonly IMeteoFranceFileReaderGeneric<DailyStationData> meteoFranceFileReader;
        private readonly IDepartmentService departmentService;
        private readonly IWebHostEnvironment environment;
        private readonly ILogger<MeteoFranceFileApiService> logger;

        public MeteoFranceFileApiService(
            IMeteoFranceLiveApiService meteoFranceLiveApiService,
            IMeteoFranceFileReaderGeneric<DailyStationData> meteoFranceFileReader,
            IDepartmentService departmentService,
            IWebHostEnvironment environment,
            ILogger<MeteoFranceFileApiService> logger)
        {
            this.meteoFranceLiveApiService = meteoFranceLiveApiService;
            this.meteoFranceFileReader = meteoFranceFileReader;
            this.departmentService = departmentService;
            this.environment = environment;
            this.logger = logger;
        }

        public async Task<StationsPayload> GetStationsAsync(string departmentId)
        {
            this.logger.LogInformation($"{nameof(GetStationsAsync)}({departmentId})");
            var filePath = this.departmentService.DepartmentFilePath(departmentId);

            if (!File.Exists(filePath))
            {
                var stationsPayload = await this.meteoFranceLiveApiService.GetStationsAsync(departmentId);
                var jsonWrite = JsonSerializer.Serialize(stationsPayload.Where(s => s.PosteOuvert));

                WriteAllText(filePath, jsonWrite);
                this.departmentService.RefreshDepartmentsStatus();
            }

            this.logger.LogInformation($"File {filePath} exists.");
            var jsonRead = File.ReadAllText(filePath);
            var stations = JsonSerializer.Deserialize<IReadOnlyCollection<Station>>(jsonRead);

            return new StationsPayload(this.departmentService.GetDepartment(departmentId), stations);
        }

        public async Task<StationPayload> GetStationDataAsync(string stationId, int year)
        {
            this.logger.LogInformation($"{nameof(GetStationDataAsync)}({stationId}, {year})");
            var filePath = Path.Combine(this.environment.WebRootPath, MeteoFranceConfig.FolderPath, "stations", stationId, $"{year}.csv");

            if (!File.Exists(filePath))
            {
                var beginDate = new DateTime(year, 1, 1);
                var endDate = new DateTime(year, 12, 31, 23, 59, 59);
                var csvData = await this.meteoFranceLiveApiService.GetStationDataAsync(stationId, MeteoFranceConfig.CommandTypeDaily, beginDate, endDate);
                WriteAllText(filePath, csvData);
            }

            var csv = File.ReadAllText(filePath);
            var data = this.meteoFranceFileReader.ParseCsv(csv).ToArray();

            var department = this.departmentService.GetDepartmentFromStationId(stationId);
            var stationsPayload = await this.GetStationsAsync(department.Code);
            var station = stationsPayload.Stations.First(s => s.Id == stationId);
            return new StationPayload(department, station, data);
        }

        private static void WriteAllText(string filePath, string data)
        {
            EnsureDirectory(filePath);
            File.WriteAllText(filePath, data);
        }

        private static void EnsureDirectory(string filePath)
        {
            var directory = Path.GetDirectoryName(filePath);
            if (Directory.Exists(directory))
                return;

            Directory.CreateDirectory(directory);

        }
    }
}
