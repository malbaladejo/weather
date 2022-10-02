using System.Globalization;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace WeatherApp.JsonConverters
{
    public class DateTimeJsonConverter : JsonConverter<DateTime>
    {
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return DateTime.Parse(reader.GetString(), CultureInfo.InvariantCulture);
        }

        public override void Write(Utf8JsonWriter writer, DateTime timeSpanValue, JsonSerializerOptions options)
        {
            writer.WriteStringValue(timeSpanValue.ToString(@"yyyy-MM-dd HH:mm"));
        }
    }
}
