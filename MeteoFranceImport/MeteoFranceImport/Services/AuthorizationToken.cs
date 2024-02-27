using System.Text.Json.Serialization;

internal class AuthorizationToken
{
	[JsonPropertyName("access_token")]
	public string Token { get; set; }
}
