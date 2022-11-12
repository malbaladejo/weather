using WeatherApp.JsonConverters;
using WeatherApp.Models;
using WeatherApp.Services;
using WeatherApp.ViewModels.DateContexts;

namespace WeatherApp.ViewModels
{
    internal class WindDirectionDayWeatherDataViewModel : WeatherDataViewModelBase
    {
        private readonly DateContext dateContext;
        private readonly IWeatherService weatherService;

        public WindDirectionDayWeatherDataViewModel(ControllerActionContext controllerContext, DateContext dateContext, IWeatherService weatherService)
            : base(controllerContext, dateContext, weatherService)
        {
            this.dateContext = dateContext;
            this.weatherService = weatherService;
        }

        public override string Title => this.dateContext.Label;

        public override async Task InitializeAsync()
        {
            var data = await this.weatherService.GetWeatherDataAsync(this.dateContext.BeginDate, this.dateContext.EndDate);
            var windDirectionData = this.CalculateWindDirections(data);
            this.JsonData = LocalJsonSerializer.Serialize(windDirectionData);
        }

        private IEnumerable<WindDirectionData> CalculateWindDirections(IReadOnlyCollection<WeatherData> data)
        {
            var windDirectionsOrders = new List<string>
            {
                "N",
                "NE",
                "E",
                "SE",
                "S",
                "SW",
                "W",
                "NW"
            };

            var directions = data.GroupBy(i => i.WindDirection)
                  .Select(g => new WindDirectionData(g.Key, Math.Round((decimal)(g.Count() * 100) / (decimal)data.Count, 1)))
                  .ToArray();

            foreach (var directionCode in windDirectionsOrders)
            {
                var direction = directions.FirstOrDefault(d => d.Direction == directionCode);
                direction = direction ?? new WindDirectionData( directionCode,  0 );
                yield return direction;
            }
        }
    }
}
