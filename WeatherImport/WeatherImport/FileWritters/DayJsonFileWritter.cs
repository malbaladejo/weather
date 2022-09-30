using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace WeatherImport
{
    internal class DayJsonFileWritter : IJsonFileWritter
    {
        public void Write(string outFolder, IReadOnlyCollection<DayDto> data)
        {
            Writefiles(outFolder, data, true);
            Writefiles(outFolder, data, false);
        }

        private static void Writefiles(string outFolder, IReadOnlyCollection<DayDto> data, bool minified)
        {
            var serializeOptions = new JsonSerializerOptions
            {
                Converters = { new TimeSpanJsonConverter(), new DateTimeJsonConverter() },
                WriteIndented = !minified
            };

            foreach (var dayData in data)
            {
                var filePath = Path.Join(outFolder, minified ? "min" : string.Empty, $"day-{dayData.Date.ToString("yyyy-MM-dd")}{(minified ? "-min" : string.Empty)}.json");

                var json = JsonSerializer.Serialize(dayData, serializeOptions);

                using (var sw = new StreamWriter(filePath))
                {
                    sw.Write(json);
                }
            }
        }
    }
}
