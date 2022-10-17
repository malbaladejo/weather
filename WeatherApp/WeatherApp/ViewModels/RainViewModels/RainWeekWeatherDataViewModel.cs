using System.Globalization;
using WeatherApp.Extensions;
using WeatherApp.JsonConverters;
using WeatherApp.Models;
using WeatherApp.Services;
using WeatherApp.ViewModels.DateContexts;

namespace WeatherApp.ViewModels.RainViewModels
{
    internal class RainWeekWeatherDataViewModel : WeatherDataViewModelBase
    {
        private readonly DateContext dateContext;
        private readonly IWeatherService weatherService;

        public RainWeekWeatherDataViewModel(ControllerActionContext controllerContext, DateContext dateContext, IWeatherService weatherService)
            : base(controllerContext, dateContext, weatherService)
        {
            this.dateContext = dateContext;
            this.weatherService = weatherService;
        }

        public override string Title => $"{this.dateContext.BeginDate.ToShortDateString()} - {this.dateContext.EndDate.ToShortDateString()}";

        public override async Task InitializeAsync()
        {      
            var data = await this.weatherService.GetWeatherDataAsync(this.dateContext.BeginDate, this.dateContext.EndDate);

            var filteredData = data.GroupBy(d => d.Date.Date)
                                    .Select(d => new WeatherData
                                    {
                                        Date = d.Key,
                                        Rain = d.Sum(v => v.Rain)
                                    })
                                    .Select(d => new RainData(d, d.Date.ToString("dd/mm/yyyy")));

            this.JsonData = LocalJsonSerializer.Serialize(filteredData);
        }
    }
}
