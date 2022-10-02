using WeatherApp.JsonConverters;
using WeatherApp.Models;

namespace WeatherApp.Controllers.Home
{
    public class HomeWeekTemperatureViewModel
    {
        public HomeWeekTemperatureViewModel(DateTime minDate, DateTime maxDate, DateTime selectedDate, IReadOnlyCollection<WeatherData> data)
        {
            this.MinDate = minDate;
            this.MaxDate = maxDate;
            this.Date = selectedDate;
            var dates = data.GroupBy(d => d.Date.Date);

            var values = dates.Select(g =>
                g.Select((d, i) => new { Data = d, Index = i })
                 .Where(d => d.Index % 7 == 0)
                 .Select(d => d.Data)
                 .Select(d => new WeekTemperatureData(d.Date.Date, d.InTemperature.Value, d.OutTemperature.Value)))
                .ToArray();
                //.se;



            //var values = dates.Select(g => new WeekTemperatureData(g.Key, g.Min(d => d.InTemperature).Value, g.Min(d => d.OutTemperature).Value))
            //    .Concat(dates.Select(g => new WeekTemperatureData(g.Key, g.Max(d => d.InTemperature).Value, g.Max(d => d.OutTemperature).Value)))
            //    .OrderBy(d => d.Date);

            this.JsonData = LocalJsonSerializer.Serialize(values);

            if (selectedDate > minDate)
                this.PreviousDate = selectedDate.AddDays(-7);

            if (selectedDate.AddDays(7) < maxDate)
                this.NextDate = selectedDate.AddDays(7);
        }

        public string JsonData { get; }
        public DateTime MinDate { get; }
        public DateTime MaxDate { get; }
        public DateTime Date { get; }
        public DateTime? PreviousDate { get; }
        public DateTime? NextDate { get; }
    }
}
