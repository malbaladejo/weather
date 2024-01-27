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
                    if (dayData != null)
                        yield return dayData;
                }
            }
            stopwatch.Stop();
            this.logger.LogInformation($"File {inputFile} loaded in {stopwatch.ElapsedMilliseconds}ms.");
        }

        private WeatherData ConvertLineToDto(string line)
        {
            var lineData = line.Split("\t");

            try
            {
                return new WeatherData
                {
                    Date = ParseDate(GetData(lineData, CsvColumns.Time)),
                    OutHumidity = ParseInt(GetData(lineData, CsvColumns.Humi)),
                    InHumidity = ParseInt(GetData(lineData, CsvColumns.InHumi)),
                    OutTemperature = ParseDecimal(GetData(lineData, CsvColumns.Temp)),
                    InTemperature = ParseDecimal(GetData(lineData, CsvColumns.InTemp)),
                    Rain = ParseDecimal(GetData(lineData, CsvColumns.RainHour)),
                    Wind = ParseDecimal(GetData(lineData, CsvColumns.Wind)),
                    WindDirection = GetData(lineData, CsvColumns.WindDirection),
                    AbsolutePressure = ParseDecimal(GetData(lineData, CsvColumns.ABSPressure)),
                    RelativePressure = ParseDecimal(GetData(lineData, CsvColumns.RELPressure))
                };
            }
            catch (Exception e)
            {
                this.logger.LogError(e, $"{string.Join(',', lineData)}");
                return null;
            }
        }

        private string GetData(string[] lineData, int columnIndex)
        {
            if (lineData.Length <= columnIndex)
            {
                this.logger.LogWarning($"{string.Join(',', lineData)} - length:{lineData.Length}, required column:{columnIndex}");
                return null;
            }

            return lineData[columnIndex];
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
