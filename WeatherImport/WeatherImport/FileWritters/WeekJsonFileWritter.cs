using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace WeatherImport
{
    internal class WeekJsonFileWritter : IJsonFileWritter
    {
        public void Write(string outFolder, IReadOnlyCollection<DayDto> data)
        {
            var weeks = new List<WeekDto>();

            foreach (var dayData in data)
            {
                var week = this.GetWeekDto(dayData, weeks);
                var weekData = this.GetWeekDataDto(dayData, week);

                weekData.Min = this.GetMinData(dayData);
                weekData.Max = this.GetMaxData(dayData);
                weekData.Rain = dayData.Data.Sum(d => d.Rain);
            }

            this.CalculateWindDirections(data, weeks);


            WriteFiles(outFolder, weeks, true);
            WriteFiles(outFolder, weeks, false);
        }

        private static void WriteFiles(string outFolder, List<WeekDto> weeks, bool minified)
        {
            var serializeOptions = new JsonSerializerOptions
            {
                Converters = { new TimeSpanJsonConverter(), new DateTimeJsonConverter() },
                WriteIndented = !minified,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            };

            foreach (var week in weeks)
            {
                var filePath = Path.Join(outFolder, minified ? "min" : string.Empty, $"week-{week.FirstDayOfWeek.ToString("yyyy-MM-dd")}-{week.LastDayOfWeek.ToString("yyyy-MM-dd")}{(minified ? "-min" : string.Empty)}.json");
                var json = JsonSerializer.Serialize(week, serializeOptions);

                using (var sw = new StreamWriter(filePath))
                {
                    sw.Write(json);
                }
            }
        }

        private WeekDto GetWeekDto(DayDto day, List<WeekDto> weeks)
        {
            var firstDayOfWeek = day.Date.FirstDayOfWeek().BeginOfDay();

            var week = weeks.FirstOrDefault(w => w.FirstDayOfWeek == firstDayOfWeek);
            if (week == null)
            {
                week = new WeekDto { FirstDayOfWeek = firstDayOfWeek, LastDayOfWeek = day.Date.LastDayOfWeek().BeginOfDay() };
                weeks.Add(week);
            }

            return week;
        }

        private WeekDataDto GetWeekDataDto(DayDto day, WeekDto week)
        {
            var weekData = week.Data.FirstOrDefault(w => w.Date == day.Date);
            if (weekData == null)
            {
                weekData = new WeekDataDto { Date = day.Date };
                week.Data.Add(weekData);
            }

            return weekData;
        }

        private DayDataDto GetMinData(DayDto dayData)
        {
            return new DayDataDto
            {
                InHumidity = dayData.Data.Min(d => d.InHumidity),
                InTemperature = dayData.Data.Min(d => d.InTemperature),
                OutHumidity = dayData.Data.Min(d => d.OutHumidity),
                OutTemperature = dayData.Data.Min(d => d.OutTemperature),
                Wind = dayData.Data.Min(d => d.Wind)
            };
        }

        private DayDataDto GetMaxData(DayDto dayData)
        {
            return new DayDataDto
            {
                InHumidity = dayData.Data.Max(d => d.InHumidity),
                InTemperature = dayData.Data.Max(d => d.InTemperature),
                OutHumidity = dayData.Data.Max(d => d.OutHumidity),
                OutTemperature = dayData.Data.Max(d => d.OutTemperature),
                Wind = dayData.Data.Max(d => d.Wind)
            };
        }

        private void CalculateWindDirections(IReadOnlyCollection<DayDto> data, List<WeekDto> weeks)
        {
            var windDirectionsOrders = new Dictionary<string, int>
            {
                ["N"] = 0,
                ["NE"] = 1,
                ["E"] = 2,
                ["SE"] = 2,
                ["S"] = 3,
                ["SW"] = 5,
                ["W"] = 6,
                ["NW"] = 7,
            };

            foreach (var week in weeks)
            {
                week.WindDirections.AddRange(
                    data.Where(d => d.Date >= week.FirstDayOfWeek && d.Date <= week.LastDayOfWeek.EndOfDay())
                    .SelectMany(d => d.Data)
                    .GroupBy(i => i.WindDirection)
                    .OrderBy(g => windDirectionsOrders[g.Key])
                      .Select(g => new WindDirection { Direction = g.Key, Percent = g.Count() }));
            }
        }
    }
}
