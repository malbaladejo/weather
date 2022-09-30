using System.Text.Json.Serialization;

namespace WeatherImport
{
    internal class WindDirection
    {
        [JsonPropertyName("d")]
        public string Direction { get; set; }

        [JsonPropertyName("p")]
        public decimal Percent { get; set; }
    }
}
