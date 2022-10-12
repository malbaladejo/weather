using WeatherApp.JsonConverters;
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

        public override string Title => $"{this.dateContext.BeginDate.ToShortDateString()} - {this.dateContext.EndDate.ToShortDateString()}";

        public override async Task InitializeAsync()
        {
            var data = await this.weatherService.GetWeatherDataAsync(this.dateContext.BeginDate,this.dateContext.EndDate);
            this.JsonData = LocalJsonSerializer.Serialize(data);
        }
    }
}
