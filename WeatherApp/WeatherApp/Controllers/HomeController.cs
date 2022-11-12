using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WeatherApp.Models;
using WeatherApp.ViewModels;

namespace WeatherApp.Controllers
{
    public class HomeController : ControllerBase
    {
        public HomeController(IWeatherDataViewModelFactory weatherDataViewModelFactory)
            :base(ControllerNames.Temperature, weatherDataViewModelFactory)
        {
        }
    }
}