using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace WeatherImport
{
    internal class WeekDto
    {
        [JsonPropertyName("type")]
        public string Type => "week";

        [JsonPropertyName("firstDay")]
        public DateTime FirstDayOfWeek { get; set; }

        [JsonPropertyName("lastDay")]
        public DateTime LastDayOfWeek { get; set; }

        [JsonPropertyName("data")]
        public List<WeekDataDto> Data { get; } = new List<WeekDataDto>();

        [JsonPropertyName("wd")]
        public List<WindDirection> WindDirections { get; } = new List<WindDirection>();
    }
}
