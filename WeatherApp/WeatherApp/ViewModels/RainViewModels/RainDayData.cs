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

        public TimeSpan Date => this.data.Date.TimeOfDay;

        public string Label => this.Date.ToString();

        public decimal? Rain => this.data.Rain;
    }
}
