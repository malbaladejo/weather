using WeatherApp.Models;

namespace WeatherApp.ViewModels
{
    public class TemperatureDayData
    {
        private readonly WeatherData data;

        public TemperatureDayData(WeatherData data)
        {
            this.data = data;
        }

        private TimeSpan Date => this.data.Date.TimeOfDay;

        public string Label => this.data.Date.ToString("HH:mm");

        public decimal? OutTemperature => this.data.OutTemperature;

        public decimal? InTemperature => this.data.InTemperature;
    }
}