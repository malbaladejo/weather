using WeatherApp.JsonConverters;
using WeatherApp.Models;
using WeatherApp.Services;
using WeatherApp.ViewModels.DateContexts;

namespace WeatherApp.ViewModels
{
    internal class TemperatureYearWeatherDataViewModel : WeatherDataViewModelBase
    {
        private readonly DateContext dateContext;
        private readonly IWeatherService weatherService;

        public TemperatureYearWeatherDataViewModel(ControllerActionContext controllerContext, DateContext dateContext, IWeatherService weatherService)
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
                    .Select(d => new TemperatureData(d, this.dateContext.GetLabel(d.Date)));

            this.JsonData = LocalJsonSerializer.Serialize(filteredData);
        }

        private IEnumerable<WeatherData> GetData(IReadOnlyCollection<WeatherData> data)
        {
            foreach (var month in data.Where(d => d.InTemperature.HasValue)
                            .Where(d => d.OutTemperature.HasValue)
                            .GroupBy(d => d.Date.Month))
            {
                var min = month.FirstOrDefault(d1 => d1.OutTemperature == month.Min(d2 => d2.OutTemperature));
                var max = month.FirstOrDefault(d1 => d1.OutTemperature == month.Max(d2 => d2.OutTemperature));

                yield return min;
                yield return max;

                var sampleData = month.Select((d, i) => new { Index = i, Data = d })
                 //.Where(d => d.Index % 50 == 0)
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
