using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WeatherApp.Models;
using WeatherApp.ViewModels;

namespace WeatherApp.Controllers
{
    public class WindController : Controller
    {
        private readonly IWeatherDataViewModelFactory weatherDataViewModelFactory;

        public WindController(IWeatherDataViewModelFactory weatherDataViewModelFactory)
        {
            this.weatherDataViewModelFactory = weatherDataViewModelFactory;
        }

        public async Task<IActionResult> Index(DateTime? date)
        {
            var controllerContext = new ControllerActionContext(ControllerNames.Wind, ActionNames.Day);
            var viewModel = await weatherDataViewModelFactory.CreateAsync(controllerContext, date, Period.Day);
            return View(viewModel);
        }

        public async Task<IActionResult> Week(DateTime? date)
        {
            var controllerContext = new ControllerActionContext(ControllerNames.Wind, ActionNames.Week);
            var viewModel = await weatherDataViewModelFactory.CreateAsync(controllerContext, date, Period.Week);
            return View("Index", viewModel);
        }

        public async Task<IActionResult> Month(DateTime? date)
        {
            var controllerContext = new ControllerActionContext(ControllerNames.Wind, ActionNames.Month);
            var viewModel = await weatherDataViewModelFactory.CreateAsync(controllerContext, date, Period.Month);
            return View("Index", viewModel);
        }

        public async Task<IActionResult> Year(DateTime? date)
        {
            var controllerContext = new ControllerActionContext(ControllerNames.Wind, ActionNames.Year);
            var viewModel = await weatherDataViewModelFactory.CreateAsync(controllerContext, date, Period.Year);
            return View("Index", viewModel);
        }
    }
}