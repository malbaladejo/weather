﻿using WeatherApp.JsonConverters;
using WeatherApp.Models;
using WeatherApp.Services;
using WeatherApp.ViewModels.DateContexts;

namespace WeatherApp.ViewModels
{
    internal class WindYearWeatherDataViewModel : WeatherDataViewModelBase
    {
        private readonly DateContext dateContext;
        private readonly IWeatherService weatherService;

        public WindYearWeatherDataViewModel(ControllerActionContext controllerContext, DateContext dateContext, IWeatherService weatherService)
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
            foreach (var month in data.Where(d => d.Wind.HasValue)
                                    .GroupBy(d => d.Date.Month))
            {
                var min = month.FirstOrDefault(d1 => d1.Wind == month.Min(d2 => d2.Wind));
                var max = month.FirstOrDefault(d1 => d1.Wind == month.Max(d2 => d2.Wind));

                yield return min;
                yield return max;

                var sampleData = month.Select((d, i) => new { Index = i, Data = d })
                 .Where(d => d.Index % 200 == 0)
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
