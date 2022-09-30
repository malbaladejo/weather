using System;
using System.Text.Json.Serialization;

namespace WeatherImport
{
    internal class WeekDataDto
    {
        [JsonPropertyName("d")]
        public DateTime Date { get; set; }

        [JsonPropertyName("min")]
        public DayDataDto Min { get; set; }

        [JsonPropertyName("max")]
        public DayDataDto Max { get; set; }

        [JsonPropertyName("r")]
        public decimal? Rain { get; set; }
    }
}
