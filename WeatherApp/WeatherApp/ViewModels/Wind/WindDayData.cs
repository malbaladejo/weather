using WeatherApp.Models;

namespace WeatherApp.ViewModels.Wind
{
    public class WindDayData
    {
        private readonly WeatherData data;

        public WindDayData(WeatherData data)
        {
            this.data = data;
        }

        private TimeSpan Date => this.data.Date.TimeOfDay;

        public string Label => this.data.Date.ToString("HH:mm");

        public decimal? Wind => this.data.Wind;
    }
}