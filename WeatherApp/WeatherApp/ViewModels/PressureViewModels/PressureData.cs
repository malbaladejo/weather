using WeatherApp.Models;

namespace WeatherApp.ViewModels
{
    public class PressureData
    {
        private readonly WeatherData data;

        public PressureData(WeatherData data, string label)
        {
            this.data = data;
            this.Label = label;
        }

        private DateTime Date => this.data.Date;

        public decimal? RelativePressure => this.data.RelativePressure;

        public decimal? AbsolutePressure => this.data.AbsolutePressure;

        public string Label { get; }
    }
}
