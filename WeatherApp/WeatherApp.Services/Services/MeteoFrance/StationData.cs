using System.Text.Json.Serialization;

namespace WeatherApp.Services
{
    public abstract class StationData
    {
        [CsvHeader("POSTE")]
        [JsonPropertyName("post")]
        public string Post { get; set; }

        [CsvHeader("DATE")]
        [JsonPropertyName("date")]
        public DateTime Date { get; set; }
    }
}
