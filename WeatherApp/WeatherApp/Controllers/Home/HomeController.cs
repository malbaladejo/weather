using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WeatherApp.Extensions;
using WeatherApp.Models;
using WeatherApp.Services;

namespace WeatherApp.Controllers.Home
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWeatherService weatherService;

        public HomeController(ILogger<HomeController> logger, IWeatherService weatherService)
        {
            _logger = logger;
            this.weatherService = weatherService;
        }

        public async Task<IActionResult> Index(DateTime? date)
        {
            var viewModel = await BuildHomeDayTemperatureViewModelAsync(date);
            return View(viewModel);
        }

        private async Task<HomeDayTemperatureViewModel> BuildHomeDayTemperatureViewModelAsync(DateTime? date)
        {
            var minDate = await this.weatherService.GetMinDateRecordAsync();
            var maxDate = await this.weatherService.GetMaxDateRecordAsync();

            var selectedDate = date.HasValue ? date.Value : maxDate;

            var data = await this.weatherService.GetWeatherDataAsync(selectedDate);
            var viewModel = new HomeDayTemperatureViewModel(minDate, maxDate, selectedDate, data);
            return viewModel;
        }

        public async Task<IActionResult> Week(DateTime? date)
        {
            var viewModel = await BuildHomeWeekTemperatureViewModelAsync(date);
            return View(viewModel);
        }



        private async Task<HomeWeekTemperatureViewModel> BuildHomeWeekTemperatureViewModelAsync(DateTime? date)
        {
            var minDate = await this.weatherService.GetMinDateRecordAsync();
            var maxDate = await this.weatherService.GetMaxDateRecordAsync();

            var selectedDate = date.HasValue ? date.Value : maxDate;
            var firstDayOfWeek = selectedDate.FirstDayOfWeek();
            var lastDayOfWeek = selectedDate.LastDayOfWeek();

            var data = await this.weatherService.GetWeatherDataAsync(firstDayOfWeek, lastDayOfWeek);
            var viewModel = new HomeWeekTemperatureViewModel(minDate, maxDate, firstDayOfWeek, data);
            return viewModel;
        }

        public async Task<IActionResult> Month(DateTime? date)
        {
            var viewModel = await BuildHomeDayTemperatureViewModelAsync(date);
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