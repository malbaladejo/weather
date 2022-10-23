using WeatherApp.Models;

namespace WeatherApp.ViewModels.RainViewModels
{
    public class RainDayData
    {
        private readonly WeatherData data;

        public RainDayData(WeatherData data)
        {
            this.data = data;
        }

        private TimeSpan Date => this.data.Date.TimeOfDay;

        public string Label => this.data.Date.ToString("HH:mm");

        public decimal? Rain => this.data.Rain;
    }
}
