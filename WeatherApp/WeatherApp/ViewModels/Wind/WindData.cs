using System.Diagnostics;
using WeatherApp.Models;

namespace WeatherApp.ViewModels.Wind
{
    [DebuggerDisplay("{Date} - Wind: {Wind}")]
    public class WindData
    {
        private readonly WeatherData data;

        public WindData(WeatherData data, string label)
        {
            this.data = data;
            this.Label = label;
        }

        private DateTime Date => this.data.Date;

        public decimal? Wind => this.data.Wind;

        public string Label { get; }
    }
}