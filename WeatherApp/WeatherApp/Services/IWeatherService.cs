using WeatherApp.Models;

namespace WeatherApp.Services
{
    public interface IWeatherService
    {
        Task<IReadOnlyCollection<WeatherData>> GetWeatherDataAsync(DateTime date);

        Task<IReadOnlyCollection<WeatherData>> GetWeatherDataAsync(DateTime startDate, DateTime endDate);

        Task ResetCacheAsync();
    }
}
