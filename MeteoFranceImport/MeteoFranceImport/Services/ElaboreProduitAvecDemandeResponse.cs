using System.Text.Json.Serialization;

public class ElaboreProduitAvecDemandeResponse
{
	[JsonPropertyName("return")]
	public string Return { get; set; }
}