using System.Text.Json.Serialization;

public class Station
{
	[JsonPropertyName("id")]
	public string Id { get; set; }

	[JsonPropertyName("nom")]
	public string Name { get; set; }

	[JsonPropertyName("posteOuvert")]
	public bool PosteOuvert { get; set; }

	[JsonPropertyName("typePoste")]
	public int TypePoste { get; set; }

	[JsonPropertyName("lon")]
	public double Longitude { get; set; }

	[JsonPropertyName("lat")]
	public double Latitude { get; set; }

	[JsonPropertyName("alt")]
	public int Alt { get; set; }

	[JsonPropertyName("postePublic")]
	public bool PostePublic { get; set; }
}
