using WeatherApp.Models;

namespace WeatherApp.ViewModels
{
    public class HumidityData
    {
        private readonly WeatherData data;

        public HumidityData(WeatherData data, string label)
        {
            this.data = data;
            this.Label = label;
        }

        private DateTime Date => this.data.Date;

        public decimal? InHumidity => this.data.InHumidity;

        public decimal? OutHumidity => this.data.OutHumidity;

        public string Label { get; }
    }
}
