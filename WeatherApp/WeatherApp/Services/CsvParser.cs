using System.Diagnostics;
using System.Globalization;
using WeatherApp.Models;

namespace WeatherApp.Services
{
    internal class CsvParser : ICsvParser
    {
        private readonly ILogger<CsvWeatherService> logger;
        private static readonly CultureInfo dateCultureInfo = new CultureInfo("fr-FR");
        private static readonly CultureInfo decimalCultureInfo = new CultureInfo("en-US");

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
            var stopwatch = new Stopwatch();
            stopwatch.Start();
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
            stopwatch.Stop();
            this.logger.LogInformation($"File {inputFile} loaded in {stopwatch.ElapsedMilliseconds}ms.");
        }

        private WeatherData ConvertLineToDto(string line)
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
                Gust = ParseDecimal(lineData[CsvColumns.Gust]),
                WindDirection = lineData[CsvColumns.WindDirection],
                AbsolutePressure = ParseDecimal(lineData[CsvColumns.ABSPressure]),
                RelativePressure = ParseDecimal(lineData[CsvColumns.RELPressure])
            };
        }

        private static DateTime ParseDate(string value)
        {
            return DateTime.Parse(value, dateCultureInfo);
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

            if (decimal.TryParse(value, NumberStyles.Any, decimalCultureInfo, out decimal valueInt))
            {
                return valueInt;
            }

            return null;
        }
    }
}
