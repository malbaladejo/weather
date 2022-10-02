using WeatherApp.Extensions;
using WeatherApp.Models;

namespace WeatherApp.Services
{
    internal class CsvWeatherService : IWeatherService
    {
        private List<WeatherData>? data;
        public Task<IReadOnlyCollection<WeatherData>> GetWeatherDataAsync(DateTime date)
            => this.GetWeatherDataAsync(date.BeginOfDay(), date.EndOfDay());

        public Task<DateTime> GetMinDateRecordAsync()
        {
            this.EnsureData();

            return Task.FromResult(this.data.Min(d => d.Date).BeginOfDay());
        }

        public Task<DateTime> GetMaxDateRecordAsync()
        {
            this.EnsureData();

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

            foreach (var filePath in Directory.GetFiles("data", "*.csv"))
            {
                this.data.AddRange(parser.Parse(filePath));
            }
        }
    }
}
