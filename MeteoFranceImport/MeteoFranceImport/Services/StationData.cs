using CsvHelper.Configuration.Attributes;
using System.Text.Json.Serialization;

public class StationData
{
	[Name("POSTE")]
	[JsonPropertyName("post")]
	public string Post { get; set; }

	[Name("DATE")]
	[JsonPropertyName("date")]
	public DateTime Date { get; set; }

	[Name("RR")]
	[JsonPropertyName("rainInMm")]
	public double RainInMm { get; set; }
}
