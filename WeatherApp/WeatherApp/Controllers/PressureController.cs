using WeatherApp.ViewModels;

namespace WeatherApp.Controllers
{
    public class PressureController : ControllerBase
    {
        public PressureController(IWeatherDataViewModelFactory weatherDataViewModelFactory)
            : base(ControllerNames.Pressure, weatherDataViewModelFactory)
        {
        }
    }
}