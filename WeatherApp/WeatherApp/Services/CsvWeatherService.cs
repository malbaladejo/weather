using WeatherApp.Extensions;
using WeatherApp.Models;

namespace WeatherApp.Services
{
    internal class CsvWeatherService : IWeatherService
    {
        private List<WeatherData>? data;
        private readonly ILogger<CsvWeatherService> logger;
        private readonly IWebHostEnvironment environment;
        private readonly ICsvParser csvParser;

        public CsvWeatherService(
            ILogger<CsvWeatherService> logger,
            IWebHostEnvironment environment,
            ICsvParser csvParser)
        {
            this.logger = logger;
            this.environment = environment;
            this.csvParser = csvParser;
        }

        public Task<IReadOnlyCollection<WeatherData>> GetWeatherDataAsync(DateTime date)
            => this.GetWeatherDataAsync(date.BeginOfDay(), date.EndOfDay());

        public Task<IReadOnlyCollection<WeatherData>> GetWeatherDataAsync(DateTime startDate, DateTime endDate)
        {
            this.EnsureData();
            return Task.FromResult((IReadOnlyCollection<WeatherData>)this.data.Where(d => d.Date >= startDate && d.Date <= endDate).ToArray());
        }

        private void EnsureData()
        {
            if (this.data != null)
                return;
            this.data = new List<WeatherData>();

            var dataPath = Path.Combine(this.environment.WebRootPath, "data");

            foreach (var filePath in Directory.GetFiles(dataPath, "*.csv"))
            {
                this.data.AddRange(this.csvParser.Parse(filePath));
            }
        }
    }
}
