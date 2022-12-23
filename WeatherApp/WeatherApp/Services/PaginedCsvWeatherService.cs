using WeatherApp.Extensions;
using WeatherApp.Models;

namespace WeatherApp.Services
{
    internal class PaginedCsvWeatherService : IWeatherService
    {
        private readonly Dictionary<DateTime, IReadOnlyCollection<WeatherData>> data;
        private readonly ILogger<CsvWeatherService> logger;
        private readonly ICsvParser csvParser;
        private readonly string dataPath;

        public PaginedCsvWeatherService(ILogger<CsvWeatherService> logger, IWebHostEnvironment environment, ICsvParser csvParser)
        {
            this.data = new Dictionary<DateTime, IReadOnlyCollection<WeatherData>>();
            this.dataPath = Path.Combine(environment.WebRootPath, "data");
            this.logger = logger;
            this.csvParser = csvParser;
        }

        public async Task ResetCacheAsync()
        {
            this.logger.LogInformation($"Reset cache.");
            this.data.Clear();
        }

        public Task<IReadOnlyCollection<WeatherData>> GetWeatherDataAsync(DateTime date)
            => this.GetWeatherDataAsync(date.BeginOfDay(), date.EndOfDay());

        public Task<IReadOnlyCollection<WeatherData>> GetWeatherDataAsync(DateTime startDate, DateTime endDate)
        {
            var availableData = this.EnsureData(startDate, endDate);
            return Task.FromResult((IReadOnlyCollection<WeatherData>)availableData.Where(d => d.Date >= startDate && d.Date <= endDate).ToArray());
        }

        private IEnumerable<WeatherData> EnsureData(DateTime startDate, DateTime endDate)
        {
            var datesInPeriod = this.GetDatesInPeriod(startDate, endDate);

            foreach (var date in datesInPeriod)
            {
                this.logger.LogInformation($"Required data {date}.");

                if (!this.data.ContainsKey(date))
                    this.LoadFile(date);
                else
                    this.logger.LogInformation($"{date} in cache.");

                foreach (var item in this.data[date])
                    yield return item;
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

        private void LoadFile(DateTime date)
        {
            var fileName = $"weather-{date.Year}-{date.Month.ToString().PadLeft(2, '0')}.csv";
            var filePath = Path.Combine(dataPath, fileName);

            this.data[date] = this.csvParser.Parse(filePath).ToArray();
        }
    }
}
