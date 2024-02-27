using System.Text.Json.Serialization;

public class CommandStationPayload
{
	[JsonPropertyName("elaboreProduitAvecDemandeResponse")]
	public ElaboreProduitAvecDemandeResponse Response { get; set; }
}
