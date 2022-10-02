using System.Text.Json;

namespace WeatherApp.JsonConverters
{
    public static class LocalJsonSerializer
    {
        public static string Serialize(object data)
        {
            var serializeOptions = new JsonSerializerOptions
            {
                Converters = { new TimeSpanJsonConverter(), new DateTimeJsonConverter() },
                DictionaryKeyPolicy = JsonNamingPolicy.CamelCase,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

           return JsonSerializer.Serialize(data, serializeOptions);
        }
    }
}
