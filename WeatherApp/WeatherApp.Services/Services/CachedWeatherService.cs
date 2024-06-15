using Microsoft.Extensions.Logging;
using WeatherApp.Extensions;
using WeatherApp.Models;

namespace WeatherApp.Services
{
    internal class CachedWeatherService : IWeatherService
    {
        private readonly IAggreagatedWeatherService aggreagatedWeatherService;
        private readonly ILogger<CachedWeatherService> logger;
        private readonly Dictionary<string, IReadOnlyCollection<WeatherData>> cache = new Dictionary<string, IReadOnlyCollection<WeatherData>>();

        public CachedWeatherService(
                IAggreagatedWeatherService aggreagatedWeatherService,
                ILogger<CachedWeatherService> logger)
        {
            this.aggreagatedWeatherService = aggreagatedWeatherService;
            this.logger = logger;
        }

        public Task<IReadOnlyCollection<WeatherData>> GetWeatherDataAsync(DateTime date)
        {
            this.logger.LogInformation($"{nameof(GetWeatherDataAsync)}({date})");
            return this.GetWeatherDataAsync(date.BeginOfDay(), date.EndOfDay());
        }

        public async Task<IReadOnlyCollection<WeatherData>> GetWeatherDataAsync(DateTime startDate, DateTime endDate)
        {
            this.logger.LogInformation($"{nameof(GetWeatherDataAsync)}({startDate}, {endDate})");
            var data = new List<WeatherData>();

            foreach (var date in this.GetDatesInPeriod(startDate, endDate))
            {
                var key = date.ToString("yyyy-MM");

                if (!cache.ContainsKey(key))
                {
                    this.logger.LogInformation($"data are not in cache for {date})");
                    var monthData = await this.aggreagatedWeatherService.GetWeatherDataAsync(date, date.LastDayOfMonth());
                    cache[key] = monthData;
                }
                else
                {
                    this.logger.LogInformation($"data are in cache for {date})");
                }

                data.AddRange(cache[key].Where(x => x.Date >= startDate && x.Date <= endDate));
            }

            return data.OrderBy(d => d.Date).ToArray();
        }

        public async Task ResetCacheAsync()
        {
            this.cache.Clear();
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
    }
}
