using WeatherApp.Models;

namespace WeatherApp.ViewModels
{
    public class PressureDayData
    {
        private readonly WeatherData data;

        public PressureDayData(WeatherData data)
        {
            this.data = data;
        }

        private TimeSpan Date => this.data.Date.TimeOfDay;

        public string Label => this.data.Date.ToString("HH:mm");

        public decimal? RelativePressure => this.data.RelativePressure;

        public decimal? AbsolutePressure => this.data.AbsolutePressure;
    }
}
