using WeatherApp.Extensions;
using WeatherApp.JsonConverters;
using WeatherApp.Models;
using WeatherApp.Services;
using WeatherApp.ViewModels.DateContexts;

namespace WeatherApp.ViewModels
{
    internal class HumidityMonthWeatherDataViewModel : WeatherDataViewModelBase
    {
        private readonly DateContext dateContext;
        private readonly IWeatherService weatherService;

        public HumidityMonthWeatherDataViewModel(ControllerActionContext controllerContext, DateContext dateContext, IWeatherService weatherService)
            : base(controllerContext, dateContext, weatherService)
        {
            this.dateContext = dateContext;
            this.weatherService = weatherService;
        }

        public override string Title => this.dateContext.Label;

        public override async Task InitializeAsync()
        {
            var data = await this.weatherService.GetWeatherDataAsync(this.dateContext.BeginDate, this.dateContext.EndDate);
            var filteredData = data.Filter(d => d.FilterHumidity())
                                   .Select(d => new HumidityData(d, this.dateContext.GetLabel(d.Date)));

            this.JsonData = LocalJsonSerializer.Serialize(filteredData);
        }

        private IEnumerable<WeatherData> GetData(IReadOnlyCollection<WeatherData> data)
        {
            foreach (var week in data.Where(d => d.InHumidity.HasValue)
                            .Where(d => d.OutHumidity.HasValue)
                            .GroupByWeek())
            {
                var min = week.FirstOrDefault(d1 => d1.OutHumidity == week.Min(d2 => d2.OutHumidity));
                var max = week.FirstOrDefault(d1 => d1.OutHumidity == week.Max(d2 => d2.OutTemperature));

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
