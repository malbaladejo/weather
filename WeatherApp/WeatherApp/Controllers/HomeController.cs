using Microsoft.AspNetCore.Mvc;
using WeatherApp.Services;
using WeatherApp.ViewModels;

namespace WeatherApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWeatherService weatherService;

        public HomeController(IWeatherService weatherService)
        {
            this.weatherService = weatherService;
        }

        public async Task<IActionResult> Index(bool? resetCache)
        {
            if (resetCache == true)
                await this.weatherService.ResetCacheAsync();

            return View(CreateViewModelAsync());
        }

        private async Task<HomeViewModel> CreateViewModelAsync()
        {
            await Task.Delay(0);

            return new HomeViewModel();
        }
    }
}