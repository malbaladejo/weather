using System.Diagnostics;

namespace WeatherApp.Models
{
    [DebuggerDisplay("{Date} - In: {InTemperature} - Out: {OutTemperature}")]
    public class WeatherData
    {
        public DateTime Date { get; set; }

        public int? OutHumidity { get; set; }

        public int? InHumidity { get; set; }

        public decimal? OutTemperature { get; set; }

        public decimal? InTemperature { get; set; }

        public decimal? Rain { get; set; }

        public decimal? Wind { get; set; }

        public decimal? Gust { get; set; }

        public decimal? RelativePressure { get; set; }

        public decimal? AbsolutePressure { get; set; }

        public string? WindDirection { get; set; }
    }
}