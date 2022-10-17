using WeatherApp.Models;

namespace WeatherApp.ViewModels.Temperature
{
    public class TemperatureDayData
    {
        private readonly WeatherData data;

        public TemperatureDayData(WeatherData data)
        {
            this.data = data;
        }

        public TimeSpan Date => this.data.Date.TimeOfDay;

        public decimal? OutTemperature => this.data.OutTemperature;

        public decimal? InTemperature => this.data.InTemperature;
    }
}