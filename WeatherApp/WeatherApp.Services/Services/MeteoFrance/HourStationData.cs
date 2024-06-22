using System.Text.Json.Serialization;

namespace WeatherApp.Services
{

    public class HourStationData : StationData
    {
        [CsvHeader("POSTE")]
        [JsonPropertyName("post")]
        public string Post { get; set; }

        [CsvHeader("DATE")]
        [JsonPropertyName("date")]
        public DateTime Date { get; set; }

        [CsvHeader("RR1")]
        [JsonPropertyName("rainInMm")]
        public decimal RainInMm { get; set; }

        [CsvHeader("T")]
        [JsonPropertyName("temperature")]
        public decimal Temperature { get; set; }

        [CsvHeader("PMER")]
        [JsonPropertyName("pressure")]
        public decimal Pressure { get; set; }
    }
}
