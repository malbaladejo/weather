using WeatherApp.Models;

namespace WeatherApp.ViewModels.Temperature
{
    public class TemperatureData
    {
        private readonly WeatherData data;

        public TemperatureData(WeatherData data)
        {
            this.data = data;
        }

        public TimeSpan Date => this.data.Date.TimeOfDay;

        public decimal? OutTemperature => this.data.OutTemperature;

        public decimal? InTemperature => this.data.InTemperature;
    }
}
