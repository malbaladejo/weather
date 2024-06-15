using Microsoft.Extensions.Logging;
using WeatherApp.Extensions;
using WeatherApp.Models;

namespace WeatherApp.Services
{
    internal class AggreagatedWeatherService : IAggreagatedWeatherService
    {
        private readonly ILogger<AggreagatedWeatherService> logger;
        private readonly IMeteoFranceImportDataService meteoFranceImportDataService;
        private readonly IEnumerable<IWeatherFileReader> weatherFileReaders;

        public AggreagatedWeatherService(
            ILogger<AggreagatedWeatherService> logger,
            IMeteoFranceImportDataService meteoFranceImportDataService,
            IEnumerable<IWeatherFileReader> weatherFileReaders)
        {
            this.logger = logger;
            this.meteoFranceImportDataService = meteoFranceImportDataService;
            this.weatherFileReaders = weatherFileReaders;
        }

        public Task<IReadOnlyCollection<WeatherData>> GetWeatherDataAsync(DateTime date)
        {
            this.logger.LogInformation($"{nameof(GetWeatherDataAsync)}({date})");
            return this.GetWeatherDataAsync(date.BeginOfDay(), date.EndOfDay());
        }

        public async Task<IReadOnlyCollection<WeatherData>> GetWeatherDataAsync(DateTime startDate, DateTime endDate)
        {
            this.logger.LogInformation($"{nameof(GetWeatherDataAsync)}({startDate}, {endDate})");
            await this.ImportMeteoFranceData();

            var data = new List<WeatherData>();

            foreach (var date in this.GetDatesInPeriod(startDate, endDate))
            {
                foreach (var weatherFileReader in this.weatherFileReaders)
                {
                    try
                    {
                        var fileData = weatherFileReader.Parse(date.Year, date.Month);
                        data.AddRange(fileData.Where(x => x.Date >= startDate && x.Date <= endDate));
                    }
                    catch (Exception e)
                    {
                        this.logger.LogError(e.DumpAsString());
                    }
                }
            }

            return data.OrderBy(d => d.Date).ToArray();
        }

        private async Task ImportMeteoFranceData()
        {
            try
            {
                await this.meteoFranceImportDataService.ImportAsync();
            }
            catch (Exception e)
            {
                this.logger.LogError("Meteo France Import failed.");
            }
        }

        private IEnumerable<DateTime> GetDatesInPeriod(DateTime startDate, DateTime endDate)
        {
            var date = new DateTime(startDate.Year, startDate.Month, 1);
            var stopDate = new DateTime(endDate.Year, endDate.Month, 1).AddMonths(1);

            while (date < stopDate)
            {
                yield return date;
                date = date.AddMonths(1);
            }
        }

        public async Task ResetCacheAsync()
        {
            // nothing to do
        }
    }
}
