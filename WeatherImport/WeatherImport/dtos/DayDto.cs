using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace WeatherImport
{
    internal class DayDto
    {
        [JsonPropertyName("type")]
        public string Type => "day";

        [JsonPropertyName("date")]
        public DateTime Date { get; set; }

        [JsonPropertyName("firstDay")]
        public DateTime FirstDayOfWeek { get; set; }

        [JsonPropertyName("lastDay")]
        public DateTime LastDayOfWeek { get; set; }

        [JsonPropertyName("data")]
        public List<DayDataDto> Data { get; } = new List<DayDataDto>();

        [JsonPropertyName("wd")]
        public List<WindDirection> WindDirections { get; } = new List<WindDirection>();
    }
}
