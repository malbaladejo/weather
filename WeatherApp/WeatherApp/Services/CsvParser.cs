using System.Globalization;
using WeatherApp.Models;

namespace WeatherApp.Services
{
    internal class CsvParser : ICsvParser
    {
        private readonly ILogger<CsvWeatherService> logger;

        public CsvParser(ILogger<CsvWeatherService> logger)
        {
            this.logger = logger;
        }

        public IEnumerable<WeatherData> Parse(string inputFile)
        {
            if (!File.Exists(inputFile))
            {
                this.logger.LogInformation($"The file {inputFile} does not exist.");
                yield break;
            }

            this.logger.LogInformation($"Load file {inputFile}.");
            using (var sr = new StreamReader(inputFile))
            {
                var line = string.Empty;
                var first = true;
                while ((line = sr.ReadLine()) != null)
                {
                    if (first)
                    {
                        first = false;
                        continue;
                    }

                    var dayData = ConvertLineToDto(line);
                    yield return dayData;
                }
            }

            this.logger.LogInformation($"File {inputFile} loaded.");
        }

        private static WeatherData ConvertLineToDto(string line)
        {
            var lineData = line.Split("\t");

            return new WeatherData
            {
                Date = ParseDate(lineData[CsvColumns.Time]),
                OutHumidity = ParseInt(lineData[CsvColumns.Humi]),
                InHumidity = ParseInt(lineData[CsvColumns.InHumi]),
                OutTemperature = ParseDecimal(lineData[CsvColumns.Temp]),
                InTemperature = ParseDecimal(lineData[CsvColumns.InTemp]),
                Rain = ParseDecimal(lineData[CsvColumns.RainHour]),
                Wind = ParseDecimal(lineData[CsvColumns.Wind]),
                WindDirection = lineData[CsvColumns.WindDirection],
                AbsolutePressure = ParseDecimal(lineData[CsvColumns.ABSPressure]),
                RelativePressure = ParseDecimal(lineData[CsvColumns.RELPressure])
            };
        }

        private static DateTime ParseDate(string value)
        {
            var cultureinfo = new CultureInfo("fr-FR");
            return DateTime.Parse(value, cultureinfo);
        }

        private static int? ParseInt(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return null;

            if (int.TryParse(value, out int valueInt))
            {
                return valueInt;
            }

            return null;
        }

        private static decimal? ParseDecimal(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return null;
            var culture = new CultureInfo("en-US");

            if (decimal.TryParse(value, NumberStyles.Any, culture, out decimal valueInt))
            {
                return valueInt;
            }

            return null;
        }
    }
}
