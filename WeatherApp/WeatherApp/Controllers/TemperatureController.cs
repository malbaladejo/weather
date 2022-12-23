using WeatherApp.ViewModels;

namespace WeatherApp.Controllers
{
    public class TemperatureController : ControllerBase
    {
        public TemperatureController(IWeatherDataViewModelFactory weatherDataViewModelFactory)
            : base(ControllerNames.Temperature, weatherDataViewModelFactory)
        {
        }
    }
}