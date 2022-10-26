using Microsoft.Extensions.Hosting;
using WeatherApp.Extensions;
using WeatherApp.Models;

namespace WeatherApp.Services
{
    internal class CsvWeatherService : IWeatherService
    {
        private List<WeatherData>? data;
        private readonly ILogger<CsvWeatherService> logger;
        private readonly IWebHostEnvironment environment;

        public CsvWeatherService(ILogger<CsvWeatherService> logger, IWebHostEnvironment environment)
        {
            this.logger = logger;
            this.environment = environment;
        }

        public Task<IReadOnlyCollection<WeatherData>> GetWeatherDataAsync(DateTime date)
            => this.GetWeatherDataAsync(date.BeginOfDay(), date.EndOfDay());

        public Task<DateTime> GetMinDateRecordAsync()
        {
            this.EnsureData();

            if (this.data.Count == 0)
                return Task.FromResult(DateTime.Now);

            return Task.FromResult(this.data.Min(d => d.Date).BeginOfDay());
        }

        public Task<DateTime> GetMaxDateRecordAsync()
        {
            this.EnsureData();
            if (this.data.Count == 0)
                return Task.FromResult(DateTime.Now);

            return Task.FromResult(this.data.Max(d => d.Date).EndOfDay());
        }

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
            var parser = new CsvParser();

            var dataPath = Path.Combine(this.environment.WebRootPath, "Data");

            foreach (var filePath in Directory.GetFiles(dataPath, "*.csv"))
            {
                this.data.AddRange(parser.Parse(filePath));
            }
        }
    }
}
