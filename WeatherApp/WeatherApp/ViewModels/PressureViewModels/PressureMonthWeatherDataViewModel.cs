using System.Globalization;
using WeatherApp.Extensions;
using WeatherApp.JsonConverters;
using WeatherApp.Models;
using WeatherApp.Services;
using WeatherApp.ViewModels.DateContexts;

namespace WeatherApp.ViewModels
{
    internal class PressureMonthWeatherDataViewModel : WeatherDataViewModelBase
    {
        private readonly DateContext dateContext;
        private readonly IWeatherService weatherService;

        public PressureMonthWeatherDataViewModel(ControllerActionContext controllerContext, DateContext dateContext, IWeatherService weatherService)
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
                    .Where(d => d != null)
                    .OrderBy(d => d.Date)
                    .Select(d => new PressureData(d, this.dateContext.GetLabel(d.Date)));

            this.JsonData = LocalJsonSerializer.Serialize(filteredData);
        }

        private IEnumerable<WeatherData> GetData(IReadOnlyCollection<WeatherData> data)
        {
            foreach (var week in data.Where(d => d.RelativePressure.HasValue)
                            .Where(d => d.AbsolutePressure.HasValue)
                            .GroupByWeek())
            {
                var min = week.FirstOrDefault(d1 => d1.AbsolutePressure == week.Min(d2 => d2.AbsolutePressure));
                var max = week.FirstOrDefault(d1 => d1.AbsolutePressure == week.Max(d2 => d2.AbsolutePressure));

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
