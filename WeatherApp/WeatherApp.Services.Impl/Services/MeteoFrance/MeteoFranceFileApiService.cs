using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace WeatherApp.Services
{
    internal class MeteoFranceFileApiService : IMeteoFranceFileApiService
    {
        private readonly IMeteoFranceLiveApiService meteoFranceLiveApiService;
        private readonly IDepartmentService departmentService;
        private readonly IWebHostEnvironment environment;
        private readonly ILogger<MeteoFranceFileApiService> logger;

        public MeteoFranceFileApiService(
            IMeteoFranceLiveApiService meteoFranceLiveApiService,
            IDepartmentService departmentService,
            IWebHostEnvironment environment,
            ILogger<MeteoFranceFileApiService> logger)
        {
            this.meteoFranceLiveApiService = meteoFranceLiveApiService;
            this.departmentService = departmentService;
            this.environment = environment;
            this.logger = logger;
        }

        public async Task<IReadOnlyCollection<Station>> GetStationsAsync(string departmentId)
        {
            this.logger.LogInformation($"{nameof(GetStationsAsync)}({departmentId})");
            var filePath = this.departmentService.DepartmentFilePath(departmentId);

            if (!File.Exists(filePath))
            {
                await this.RunAndRetryAsync(async () =>
                {
                    var stations = await this.meteoFranceLiveApiService.GetStationsAsync(departmentId);
                    var jsonWrite = JsonSerializer.Serialize(stations);
                    File.WriteAllText(filePath, jsonWrite);
                    this.departmentService.RefreshDepartmentsStatus();
                });
            }

            this.logger.LogInformation($"File {filePath} exists.");
            var jsonRead = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<IReadOnlyCollection<Station>>(jsonRead);
        }

        private async Task RunAndRetryAsync(Func<Task> taskAction)
        {
            const int maxTry = 3;
            const int durationInMs = 3000;
            var nbTry = 0;
            var success = false;

            do
            {
                try
                {
                    await Task.Delay(durationInMs);
                    nbTry++;
                    this.logger.LogInformation($"Try {nbTry} in progress.");
                    await taskAction.Invoke();
                    success = true;
                    this.logger.LogInformation($"Try {nbTry} successed.");
                }
                catch
                {
                    this.logger.LogInformation($"Try {nbTry} in error.");
                }
            } while (!success && nbTry < maxTry);

            if (!success)
                throw new InvalidDataException("Too many try.");
        }
    }
}
