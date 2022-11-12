using System.Diagnostics;
using WeatherApp.Models;
using WeatherApp.ViewModels;

namespace WeatherApp.Controllers
{
    public class HumidityController : ControllerBase
    {
        public HumidityController(IWeatherDataViewModelFactory weatherDataViewModelFactory)
            : base(ControllerNames.Humidity, weatherDataViewModelFactory)
        {
        }
    }
}