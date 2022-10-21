using System.Globalization;
using WeatherApp.JsonConverters;
using WeatherApp.Models;
using WeatherApp.Services;
using WeatherApp.ViewModels.DateContexts;

namespace WeatherApp.ViewModels.Temperature
{
    internal class TemperatureMonthWeatherDataViewModel : WeatherDataViewModelBase
    {
        private readonly DateContext dateContext;
        private readonly IWeatherService weatherService;

        public TemperatureMonthWeatherDataViewModel(ControllerActionContext controllerContext, DateContext dateContext, IWeatherService weatherService)
            : base(controllerContext, dateContext, weatherService)
        {
            this.dateContext = dateContext;
            this.weatherService = weatherService;
        }

        public override string Title => this.dateContext.Label;

        public override async Task InitializeAsync()
        {
            var data = await this.weatherService.GetWeatherDataAsync(this.dateContext.BeginDate, this.dateContext.EndDate);
            var filteredData = GetData(data)
                    .OrderBy(d => d.Date)
                    .Select(d => new TemperatureData(d));

            this.JsonData = LocalJsonSerializer.Serialize(filteredData);
        }

        private IEnumerable<WeatherData> GetData(IReadOnlyCollection<WeatherData> data)
        {
            var culture = new CultureInfo("en-US");
            var calendar = culture.Calendar;

            // Gets the DTFI properties required by GetWeekOfYear.
            var calendarRule = culture.DateTimeFormat.CalendarWeekRule;
            var firstDayOfWeek = culture.DateTimeFormat.FirstDayOfWeek;

            foreach (var week in data.Where(d => d.InTemperature.HasValue)
                            .Where(d => d.OutTemperature.HasValue)
                            .GroupBy(d => calendar.GetWeekOfYear(d.Date, calendarRule, firstDayOfWeek)))
            {
                var min = week.FirstOrDefault(d1 => d1.OutTemperature == week.Min(d2 => d2.OutTemperature));
                var max = week.FirstOrDefault(d1 => d1.OutTemperature == week.Max(d2 => d2.OutTemperature));

                yield return min;
                yield return max;

                var sampleData = week.Select((d, i) => new { Index = i, Data = d })
                 .Where(d => d.Index % 80 == 0)
                 .Where(d => d.Data != min)
                 .Where(d => d.Data != max)
                 .Select(d => d.Data);

                foreach (var dayData in sampleData)
                {
                    yield return dayData;
                }
            }
        }
    }
}
