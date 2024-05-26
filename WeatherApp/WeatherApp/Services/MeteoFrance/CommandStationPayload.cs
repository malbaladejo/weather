using System.Text.Json.Serialization;
namespace WeatherApp.Services
{
	public class CommandStationPayload
	{
		[JsonPropertyName("elaboreProduitAvecDemandeResponse")]
		public ElaboreProduitAvecDemandeResponse Response { get; set; }
	}
}