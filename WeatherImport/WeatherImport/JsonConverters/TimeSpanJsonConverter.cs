using System;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace WeatherImport
{
    public class TimeSpanJsonConverter : JsonConverter<TimeSpan>
    {
        public override TimeSpan Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return TimeSpan.ParseExact(reader.GetString() ?? "", @"hh\:mm\:ss", CultureInfo.InvariantCulture);
        }

        public override void Write(Utf8JsonWriter writer, TimeSpan timeSpanValue, JsonSerializerOptions options)
        {
            writer.WriteStringValue(timeSpanValue.ToString(@"hh\:mm\:ss"));
        }
    }
}
