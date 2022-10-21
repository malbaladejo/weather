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

        public TimeSpan Date => this.data.Date.TimeOfDay;

        public decimal? Wind => this.data.Wind;
    }
}