using System.Diagnostics;
using WeatherApp.Models;

namespace WeatherApp.ViewModels.Wind
{
    [DebuggerDisplay("{Date} - Wind: {Wind}")]
    public class WindData
    {
        private readonly WeatherData data;

        public WindData(WeatherData data)
        {
            this.data = data;
        }

        public DateTime Date => this.data.Date;

        public decimal? Wind => this.data.Wind;
    }
}