using System.Text.Json.Serialization;

namespace WeatherApp.Models
{
    public class WeatherData
    {
        public DateTime Date { get; set; }

        public int? OutHumidity { get; set; }

        public int? InHumidity { get; set; }

        public decimal? OutTemperature { get; set; }

        public decimal? InTemperature { get; set; }

        public decimal? Rain { get; set; }

        public decimal? Wind { get; set; }

        public string? WindDirection { get; set; }
    }
}