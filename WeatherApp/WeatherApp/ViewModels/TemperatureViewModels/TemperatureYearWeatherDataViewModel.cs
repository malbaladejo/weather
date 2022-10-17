using WeatherApp.JsonConverters;
using WeatherApp.Services;
using WeatherApp.ViewModels.DateContexts;
using WeatherApp.ViewModels.Temperature;

namespace WeatherApp.ViewModels.TemperatureViewModels
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

        public override string Title => this.dateContext.BeginDate.ToString("yyyy");

        public override async Task InitializeAsync()
        {
            var data = await this.weatherService.GetWeatherDataAsync(this.dateContext.BeginDate, this.dateContext.EndDate);
         
            var filteredData = data.Select((d, i) => new { Index = i, Data = d })
                                       .Where(d => d.Index % 60 == 0)
                                       .Select(d => new TemperatureData(d.Data));

            this.JsonData = LocalJsonSerializer.Serialize(filteredData);
        }
    }
}
