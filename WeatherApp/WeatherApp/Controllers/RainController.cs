using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WeatherApp.Models;
using WeatherApp.ViewModels;

namespace WeatherApp.Controllers
{
    public class RainController : ControllerBase
    {
        public RainController(IWeatherDataViewModelFactory weatherDataViewModelFactory)
            : base(ControllerNames.Rain, weatherDataViewModelFactory)
        {
        }
    }
}