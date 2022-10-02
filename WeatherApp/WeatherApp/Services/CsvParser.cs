using System.Globalization;
using WeatherApp.Extensions;
using WeatherApp.Models;

namespace WeatherApp.Services
{
    internal class CsvParser
    {
        public IEnumerable<WeatherData> Parse(string inputFile)
        {
            Console.WriteLine($"Parse {inputFile}.");

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
        }

        private static WeatherData ConvertLineToDto(string line)
        {
            var lineData = line.Split("\t");

            return new WeatherData
            {
                Date = DateTime.Parse(lineData[CsvColumns.Time]),
                OutHumidity = ParseInt(lineData[CsvColumns.Humi]),
                InHumidity = ParseInt(lineData[CsvColumns.InHumi]),
                OutTemperature = ParseDecimal(lineData[CsvColumns.Temp]),
                InTemperature = ParseDecimal(lineData[CsvColumns.InTemp]),
                Rain = ParseDecimal(lineData[CsvColumns.RainHour]),
                Wind = ParseDecimal(lineData[CsvColumns.Wind]),
                WindDirection = lineData[CsvColumns.WindDirection]
            };
        }

        private static int? ParseInt(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return null;

            int valueInt = 0;
            if (int.TryParse(value, out valueInt))
            {
                return valueInt;
            }

            return 0;
        }

        private static decimal? ParseDecimal(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return null;

            value = value.Replace(".", ",");

            decimal valueInt = 0;
            if (decimal.TryParse(value, out valueInt))
            {
                return valueInt;
            }

            return 0;
        }
    }
}
