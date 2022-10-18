using WeatherApp.Extensions;
using WeatherApp.JsonConverters;
using WeatherApp.Models;
using WeatherApp.Services;
using WeatherApp.ViewModels.DateContexts;

namespace WeatherApp.ViewModels.RainViewModels
{
    internal class RainYearWeatherDataViewModel : WeatherDataViewModelBase
    {
        private readonly DateContext dateContext;
        private readonly IWeatherService weatherService;

        public RainYearWeatherDataViewModel(ControllerActionContext controllerContext, DateContext dateContext, IWeatherService weatherService)
            : base(controllerContext, dateContext, weatherService)
        {
            this.dateContext = dateContext;
            this.weatherService = weatherService;
        }

        public override string Title => this.dateContext.Label;

        public override async Task InitializeAsync()
        {
            var data = await this.weatherService.GetWeatherDataAsync(this.dateContext.BeginDate, this.dateContext.EndDate);

            var filteredData = data.GroupBy(d => d.Date.Month)
                                    .Select(d => new WeatherData
                                    {
                                        Date = d.First().Date.FirstDayOfMonth(),
                                        Rain = d.Sum(v => v.Rain)
                                    })
                                    .Select(d => new RainData(d, d.Date.ToString("MMMM yyyy")));

            this.JsonData = LocalJsonSerializer.Serialize(filteredData);
        }
    }
}
