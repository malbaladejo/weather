using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace WeatherImport
{
    internal class CsvParser
    {
        private static readonly CultureInfo Culture = new CultureInfo("en-US");

        public List<DayDto> Parse(List<DayDto> data, string inputFile)
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
                    var dayDto = GetDayDto(data, line);

                    var dayData = ConvertLineToDto(line);

                    if (dayDto.Data.All(d => d.Date != dayData.Date))
                        dayDto.Data.Add(dayData);
                }
            }

            this.CalculateWindDirections(data);

            return data;
        }

        private void CalculateWindDirections(List<DayDto> data)
        {
            var windDirectionsOrders = new List<string>
            {
                "N",
                "NE",
                "E",
                "SE",
                "S",
                "SW",
                "W",
                "NW"
            };

            foreach (var item in data)
            {
                item.WindDirections.Clear();

                var directions = item.Data.GroupBy(i => i.WindDirection)
                      .Select(g => new WindDirection { Direction = g.Key, Percent = Math.Round((decimal)(g.Count() * 100) / (decimal)item.Data.Count, 1) })
                      .ToArray();

                foreach (var directionCode in windDirectionsOrders)
                {
                    var direction = directions.FirstOrDefault(d => d.Direction == directionCode);
                    direction = direction ?? new WindDirection { Direction = directionCode, Percent = 0 };
                    item.WindDirections.Add(direction);
                }
            }
        }

        private static DayDto GetDayDto(List<DayDto> data, string line)
        {
            var lineData = line.Split("\t");
            var date = DateTime.Parse(lineData[CsvColumns.Time]).Date;

            var dayDto = data.FirstOrDefault(d => d.Date == date);
            if (dayDto == null)
            {
                dayDto = new DayDto
                {
                    Date = date,
                    FirstDayOfWeek = date.FirstDayOfWeek(),
                    LastDayOfWeek = date.LastDayOfWeek()
                };
                data.Add(dayDto);
            }

            return dayDto;
        }

        private static DayDataDto ConvertLineToDto(string line)
        {
            var lineData = line.Split("\t");

            return new DayDataDto
            {
                Date = DateTime.Parse(lineData[CsvColumns.Time]).TimeOfDay,
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
