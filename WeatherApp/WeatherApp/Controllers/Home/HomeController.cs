using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WeatherApp.Extensions;
using WeatherApp.Models;
using WeatherApp.Services;
using WeatherApp.ViewModels;

namespace WeatherApp.Controllers.Home
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWeatherDataViewModelFactory weatherDataViewModelFactory;

        public HomeController(ILogger<HomeController> logger, IWeatherDataViewModelFactory weatherDataViewModelFactory)
        {
            _logger = logger;
            this.weatherDataViewModelFactory = weatherDataViewModelFactory;
        }


        public async Task<IActionResult> Index(DateTime? date)
        {
            var controllerContext = new ControllerActionContext(ControllerNames.Temperature, ActionNames.Day);
            var viewModel = await this.weatherDataViewModelFactory.CreateAsync(controllerContext, date, Period.Day);
            return View(viewModel);
        }

        public async Task<IActionResult> Week(DateTime? date)
        {
            var controllerContext = new ControllerActionContext(ControllerNames.Temperature, ActionNames.Week);
            var viewModel = await this.weatherDataViewModelFactory.CreateAsync(controllerContext, date, Period.Week);
            return View(viewModel);
        }

        public async Task<IActionResult> Month(DateTime? date)
        {
            var controllerContext = new ControllerActionContext(ControllerNames.Temperature, ActionNames.Month);
            var viewModel = await this.weatherDataViewModelFactory.CreateAsync(controllerContext, date, Period.Month);
            return View(viewModel);
        }

        public async Task<IActionResult> Year(DateTime? date)
        {
            var controllerContext = new ControllerActionContext(ControllerNames.Temperature, ActionNames.Year);
            var viewModel = await this.weatherDataViewModelFactory.CreateAsync(controllerContext, date, Period.Year);
            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}