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

        public virtual async Task<IActionResult> Index(DateTime? date, DateTime? endDate = null, bool? resetCache = null)
        {
            var controllerContext = new ControllerActionContext(this.controllerName, ActionNames.Day);
            var viewModel = await weatherDataViewModelFactory.CreateAsync(controllerContext, Period.Day, date, endDate);
            return View(viewModel);
        }

        public async Task<IActionResult> Week(DateTime? date, DateTime? endDate = null)
        {
            var controllerContext = new ControllerActionContext(this.controllerName, ActionNames.Week);
            var viewModel = await weatherDataViewModelFactory.CreateAsync(controllerContext, Period.Week, date, endDate);
            return View("Index", viewModel);
        }

        public async Task<IActionResult> Month(DateTime? date, DateTime? endDate = null)
        {
            var controllerContext = new ControllerActionContext(this.controllerName, ActionNames.Month);
            var viewModel = await weatherDataViewModelFactory.CreateAsync(controllerContext, Period.Month, date, endDate);
            return View("Index", viewModel);
        }

        public async Task<IActionResult> Year(DateTime? date, DateTime? endDate = null)
        {
            var controllerContext = new ControllerActionContext(this.controllerName, ActionNames.Year);
            var viewModel = await weatherDataViewModelFactory.CreateAsync(controllerContext, Period.Year, date, endDate);
            return View("Index", viewModel);
        }
    }
}