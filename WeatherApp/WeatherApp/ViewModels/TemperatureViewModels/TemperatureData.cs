using System.Diagnostics;
using WeatherApp.Models;

namespace WeatherApp.ViewModels.Temperature
{
    [DebuggerDisplay("{Date} - In: {InTemperature} - Out: {OutTemperature}")]
    public class TemperatureData
    {
        private readonly WeatherData data;

        public TemperatureData(WeatherData data)
        {
            this.data = data;
        }

        public DateTime Date => this.data.Date;

        public decimal? OutTemperature => this.data.OutTemperature;

        public decimal? InTemperature => this.data.InTemperature;
    }
}