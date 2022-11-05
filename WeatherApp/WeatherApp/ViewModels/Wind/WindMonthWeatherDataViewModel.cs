﻿using System.Globalization;
using WeatherApp.JsonConverters;
using WeatherApp.Models;
using WeatherApp.Services;
using WeatherApp.ViewModels.DateContexts;

namespace WeatherApp.ViewModels.Wind
{
    internal class WindMonthWeatherDataViewModel : WeatherDataViewModelBase
    {
        private readonly DateContext dateContext;
        private readonly IWeatherService weatherService;

        public WindMonthWeatherDataViewModel(ControllerActionContext controllerContext, DateContext dateContext, IWeatherService weatherService)
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
                    .Select(d => new WindData(d, this.dateContext.GetLabel(d.Date)));

            this.JsonData = LocalJsonSerializer.Serialize(filteredData);
        }

        private IEnumerable<WeatherData> GetData(IReadOnlyCollection<WeatherData> data)
        {
            var culture = new CultureInfo("en-US");
            var calendar = culture.Calendar;

            // Gets the DTFI properties required by GetWeekOfYear.
            var calendarRule = culture.DateTimeFormat.CalendarWeekRule;
            var firstDayOfWeek = culture.DateTimeFormat.FirstDayOfWeek;

            foreach (var day in data.Where(d => d.Wind.HasValue)
                            .GroupBy(d => calendar.GetWeekOfYear(d.Date, calendarRule, firstDayOfWeek)))
            {
                var min = day.FirstOrDefault(d1 => d1.Wind == day.Min(d2 => d2.Wind));
                var max = day.FirstOrDefault(d1 => d1.Wind == day.Max(d2 => d2.Wind));

                yield return min;
                yield return max;

                var sampleData = day.Select((d, i) => new { Index = i, Data = d })
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