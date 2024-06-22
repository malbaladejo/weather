using System.Text.Json.Serialization;
namespace WeatherApp.Services
{
    public class AuthorizationToken
    {
        [JsonPropertyName("access_token")]
        public string Token { get; set; }
    }
}