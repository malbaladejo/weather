using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WeatherApp.Models;
using WeatherApp.ViewModels;

namespace WeatherApp.Controllers
{
    public class WindDirectionController : ControllerBase
    {
        public WindDirectionController(IWeatherDataViewModelFactory weatherDataViewModelFactory)
            : base(ControllerNames.WinDirection, weatherDataViewModelFactory)
        {
        }
    }
}