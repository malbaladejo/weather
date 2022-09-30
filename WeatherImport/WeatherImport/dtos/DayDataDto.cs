using System;
using System.Text.Json.Serialization;

namespace WeatherImport
{
    internal class DayDataDto
    {
        [JsonPropertyName("d")]
        public TimeSpan? Date { get; set; }

        [JsonPropertyName("oh")]
        public int? OutHumidity { get; set; }

        [JsonPropertyName("ih")]
        public int? InHumidity { get; set; }

        [JsonPropertyName("ot")]
        public decimal? OutTemperature { get; set; }

        [JsonPropertyName("it")]
        public decimal? InTemperature { get; set; }

        [JsonPropertyName("r")]
        public decimal? Rain { get; set; }

        [JsonPropertyName("w")]
        public decimal? Wind { get; set; }

        [JsonPropertyName("wd")]
        public string WindDirection { get; set; }
    }
}
