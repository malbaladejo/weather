using WeatherApp.JsonConverters;
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

        public override string Title => this.dateContext.BeginDate.ToString("MMMM yyyy");

        public override async Task InitializeAsync()
        {
            var data = await this.weatherService.GetWeatherDataAsync(this.dateContext.BeginDate, this.dateContext.EndDate);
            this.JsonData = LocalJsonSerializer.Serialize(data);
        }
    }
}
