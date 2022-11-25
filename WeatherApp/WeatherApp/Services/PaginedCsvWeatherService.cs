using WeatherApp.Extensions;
using WeatherApp.Models;

namespace WeatherApp.Services
{
    internal class PaginedCsvWeatherService : IWeatherService
    {
        private readonly List<WeatherData> data = new List<WeatherData>();
        private readonly List<DateTime> loadedMonthes = new List<DateTime>();
        private readonly ILogger<CsvWeatherService> logger;
        private readonly IWebHostEnvironment environment;
        private readonly ICsvParser csvParser;

        public PaginedCsvWeatherService(ILogger<CsvWeatherService> logger, IWebHostEnvironment environment, ICsvParser csvParser)
        {
            this.logger = logger;
            this.environment = environment;
            this.csvParser = csvParser;
        }

        public Task<IReadOnlyCollection<WeatherData>> GetWeatherDataAsync(DateTime date)
            => this.GetWeatherDataAsync(date.BeginOfDay(), date.EndOfDay());

        public Task<IReadOnlyCollection<WeatherData>> GetWeatherDataAsync(DateTime startDate, DateTime endDate)
        {
            this.EnsureData(startDate, endDate);
            return Task.FromResult((IReadOnlyCollection<WeatherData>)this.data.Where(d => d.Date >= startDate && d.Date <= endDate).ToArray());
        }

        private void EnsureData(DateTime startDate, DateTime endDate)
        {
            var datesInPeriod = this.GetDatesInPeriod(startDate, endDate);
            var datesToLoad = this.GetDatesToLoad(datesInPeriod);

            foreach (var date in datesToLoad)
            {
                this.LoadFile(date);
            }
        }

        private IEnumerable<DateTime> GetDatesInPeriod(DateTime startDate, DateTime endDate)
        {
            var date = new DateTime(startDate.Year, startDate.Month, 1).AddMonths(-1);
            var stopDate = new DateTime(endDate.Year, endDate.Month, 10).AddMonths(1);

            while (date < stopDate)
            {
                yield return date;
                date = date.AddMonths(1);
            }
        }

        private IEnumerable<DateTime> GetDatesToLoad(IEnumerable<DateTime> dates)
        {
            foreach (var date in dates)
            {
                if (this.loadedMonthes.Contains(date))
                    continue;

                yield return date;
            }
        }

        private void LoadFile(DateTime date)
        {
            var dataPath = Path.Combine(this.environment.WebRootPath, "data");
            var fileName = $"weather-{date.Year}-{date.Month.ToString().PadLeft(2, '0')}.csv";
            var filePath = Path.Combine(dataPath, fileName);

            var newData = this.data.Concat(this.csvParser.Parse(filePath));
            this.data.Clear();
            this.data.AddRange(newData.OrderBy(d => d.Date));
            this.loadedMonthes.Add(date);

        }
    }
}
