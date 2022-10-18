using System.Linq;
using WeatherApp.JsonConverters;
using WeatherApp.Models;
using WeatherApp.Services;
using WeatherApp.ViewModels.DateContexts;

namespace WeatherApp.ViewModels.Temperature
{
    internal class TemperatureWeekWeatherDataViewModel : WeatherDataViewModelBase
    {
        private readonly DateContext dateContext;
        private readonly IWeatherService weatherService;

        public TemperatureWeekWeatherDataViewModel(ControllerActionContext controllerContext, DateContext dateContext, IWeatherService weatherService)
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
            foreach (var day in data.Where(d => d.InTemperature.HasValue)
                            .Where(d => d.OutTemperature.HasValue)
                            .GroupBy(d => d.Date.Date))
            {
                var min = day.FirstOrDefault(d1 => d1.OutTemperature == day.Min(d2 => d2.OutTemperature));
                var max = day.FirstOrDefault(d1 => d1.OutTemperature == day.Max(d2 => d2.OutTemperature));

                yield return min;
                yield return max;

                var sampleData = day.Select((d, i) => new { Index = i, Data = d })
                 .Where(d => d.Index % 7 == 0)
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
