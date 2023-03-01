using System.Diagnostics;
using WeatherApp.Models;

namespace WeatherApp.ViewModels
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

        public decimal? Wind => GetValueInKH(this.data.Wind);

        public decimal? Gust => GetValueInKH(this.data.Gust);

        public string Label { get; }

        private static decimal? GetValueInKH(decimal? value) => value.HasValue ? value.Value * (decimal)3.6 : null;
    }
}