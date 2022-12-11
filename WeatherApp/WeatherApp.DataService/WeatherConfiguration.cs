using System.Text.Json;

namespace WeatherApp.Ftp
{
    public class WeatherConfiguration
    {
        public string LocalFolder { get; set; }

        public string DevFolder { get; set; }

        public string User { get; set; }

        public string Password { get; set; }

        public string RemoteFolder { get; set; }

        public static WeatherConfiguration Read()
        {
            var options = new JsonSerializerOptions
            {
                DictionaryKeyPolicy = JsonNamingPolicy.CamelCase,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            return JsonSerializer.Deserialize<WeatherConfiguration>(File.ReadAllText("config.json"), options);
        }
    }
}
