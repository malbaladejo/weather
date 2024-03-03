using Microsoft.AspNetCore.Mvc;
using WeatherApp.Services;
using WeatherApp.ViewModels;

namespace WeatherApp.Controllers
{
    public class HomeController : ControllerBase
    {
        private readonly IWeatherService weatherService;

        public HomeController(IWeatherDataViewModelFactory weatherDataViewModelFactory, IWeatherService weatherService)
            : base(ControllerNames.Temperature, weatherDataViewModelFactory)
        {
            this.weatherService = weatherService;
        }

        public async override Task<IActionResult> Index(DateTime? date, DateTime? endDate, bool? resetCache)
        {
            if (resetCache == true)
                await this.weatherService.ResetCacheAsync();

            return await base.Index(date, endDate, resetCache);
        }
    }
}