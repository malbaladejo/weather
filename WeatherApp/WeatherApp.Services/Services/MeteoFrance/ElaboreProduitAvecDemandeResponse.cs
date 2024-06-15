using System.Text.Json.Serialization;
namespace WeatherApp.Services
{
	public class ElaboreProduitAvecDemandeResponse
	{
		[JsonPropertyName("return")]
		public string Return { get; set; }
	}
}