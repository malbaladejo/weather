using WeatherApp.Models;

namespace WeatherApp.ViewModels.RainViewModels
{
    public class RainData
    {
        private readonly WeatherData data;

        public RainData(WeatherData data, string label)
        {
            this.data = data;
            Label = label;
        }

        public DateTime Date => this.data.Date;

        public decimal? Rain => this.data.Rain;

        public string Label { get; }
    }
}
