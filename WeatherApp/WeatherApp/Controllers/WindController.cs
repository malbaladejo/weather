using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WeatherApp.Models;
using WeatherApp.ViewModels;

namespace WeatherApp.Controllers
{
    public class WindController : ControllerBase
    {
        public WindController(IWeatherDataViewModelFactory weatherDataViewModelFactory)
            : base(ControllerNames.Wind, weatherDataViewModelFactory)
        {
        }
    }
}