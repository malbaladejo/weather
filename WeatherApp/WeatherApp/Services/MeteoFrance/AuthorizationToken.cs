using System.Text.Json.Serialization;
namespace WeatherApp.Services
{
	internal class AuthorizationToken
	{
		[JsonPropertyName("access_token")]
		public string Token { get; set; }
	}
}