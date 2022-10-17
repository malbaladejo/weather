using WeatherApp.Services;
using WeatherApp.ViewModels.DateContexts;
using WeatherApp.ViewModels.RainViewModels;
using WeatherApp.ViewModels.Temperature;
using WeatherApp.ViewModels.TemperatureViewModels;

namespace WeatherApp.ViewModels
{
    internal class WeatherDataViewModelFactory : IWeatherDataViewModelFactory
    {
        private readonly IWeatherService weatherService;
        private readonly IDateContextFactoryStrategy dateContextFactoryStrategy;

        public WeatherDataViewModelFactory(IWeatherService weatherService, IDateContextFactoryStrategy dateContextFactoryStrategy)
        {
            this.weatherService = weatherService;
            this.dateContextFactoryStrategy = dateContextFactoryStrategy;
        }

        public async Task<IWeatherDataViewModel> CreateAsync(ControllerActionContext controllerContext, DateTime? selectedDate, Period period)
        {
            var dateContextFactory = await this.dateContextFactoryStrategy.GetFactoryAsync(period, selectedDate);
            var dateContext = dateContextFactory.Create();

            var viewModel = CreateViewModel(controllerContext, dateContext);

            await viewModel.InitializeAsync();

            return viewModel;
        }

        private IWeatherDataViewModel CreateViewModel(ControllerActionContext controllerContext, DateContext dateContext)
        {
            switch (controllerContext.Controller)
            {
                case ControllerNames.Temperature:
                    return CreateTemperatureViewModel(controllerContext, dateContext);
                case ControllerNames.Rain:
                    return CreateRainViewModel(controllerContext, dateContext);
                default:
                    throw new ArgumentOutOfRangeException($"Controller {controllerContext.Controller} not supported.");
            }
        }

        private IWeatherDataViewModel CreateTemperatureViewModel(ControllerActionContext controllerContext, DateContext dateContext)
        {
            switch (controllerContext.Action)
            {
                case ActionNames.Day:
                    return new TemperatureDayWeatherDataViewModel(controllerContext, dateContext, this.weatherService);
                case ActionNames.Week:
                    return new TemperatureWeekWeatherDataViewModel(controllerContext, dateContext, this.weatherService);
                case ActionNames.Month:
                    return new TemperatureMonthWeatherDataViewModel(controllerContext, dateContext, this.weatherService);
                case ActionNames.Year:
                    return new TemperatureYearWeatherDataViewModel(controllerContext, dateContext, this.weatherService);
                default:
                    throw new ArgumentOutOfRangeException($"Action {controllerContext.Controller}.{controllerContext.Action} not supported.");
            }
        }

        private IWeatherDataViewModel CreateRainViewModel(ControllerActionContext controllerContext, DateContext dateContext)
        {
            switch (controllerContext.Action)
            {
                case ActionNames.Day:
                    return new RainDayWeatherDataViewModel(controllerContext, dateContext, this.weatherService);
                case ActionNames.Week:
                    return new RainWeekWeatherDataViewModel(controllerContext, dateContext, this.weatherService);
                case ActionNames.Month:
                    return new RainMonthWeatherDataViewModel(controllerContext, dateContext, this.weatherService);
                case ActionNames.Year:
                    return new RainYearWeatherDataViewModel(controllerContext, dateContext, this.weatherService);
                default:
                    throw new ArgumentOutOfRangeException($"Action {controllerContext.Controller}.{controllerContext.Action} not supported.");
            }
        }
    }
}
