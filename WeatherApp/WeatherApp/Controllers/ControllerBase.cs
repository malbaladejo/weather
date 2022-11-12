using Microsoft.AspNetCore.Mvc;
using WeatherApp.ViewModels;

namespace WeatherApp.Controllers
{
    public abstract class ControllerBase : Controller
    {
        private readonly IWeatherDataViewModelFactory weatherDataViewModelFactory;
        private readonly string controllerName;

        protected ControllerBase(string controllerName, IWeatherDataViewModelFactory weatherDataViewModelFactory)
        {
            this.controllerName = controllerName;
            this.weatherDataViewModelFactory = weatherDataViewModelFactory;
        }

        public async Task<IActionResult> Index(DateTime? date)
        {
            var controllerContext = new ControllerActionContext(this.controllerName, ActionNames.Day);
            var viewModel = await weatherDataViewModelFactory.CreateAsync(controllerContext, date, Period.Day);
            return View(viewModel);
        }

        public async Task<IActionResult> Week(DateTime? date)
        {
            var controllerContext = new ControllerActionContext(this.controllerName, ActionNames.Week);
            var viewModel = await weatherDataViewModelFactory.CreateAsync(controllerContext, date, Period.Week);
            return View("Index", viewModel);
        }

        public async Task<IActionResult> Month(DateTime? date)
        {
            var controllerContext = new ControllerActionContext(this.controllerName, ActionNames.Month);
            var viewModel = await weatherDataViewModelFactory.CreateAsync(controllerContext, date, Period.Month);
            return View("Index", viewModel);
        }

        public async Task<IActionResult> Year(DateTime? date)
        {
            var controllerContext = new ControllerActionContext(this.controllerName, ActionNames.Year);
            var viewModel = await weatherDataViewModelFactory.CreateAsync(controllerContext, date, Period.Year);
            return View("Index", viewModel);
        }
    }
}