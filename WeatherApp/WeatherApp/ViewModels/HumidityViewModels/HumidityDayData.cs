using WeatherApp.Models;

namespace WeatherApp.ViewModels
{
    public class HumidityDayData
    {
        private readonly WeatherData data;

        public HumidityDayData(WeatherData data)
        {
            this.data = data;
        }

        private TimeSpan Date => this.data.Date.TimeOfDay;

        public string Label => this.data.Date.ToString("HH:mm");

        public decimal? InHumidity => this.data.InHumidity;

        public decimal? OutHumidity => this.data.OutHumidity;
    }
}
