using WeatherApp.Models;

namespace WeatherApp.Services
{
    public interface IWeatherService
    {
        //Task<DateTime> GetMinDateRecordAsync();
        //Task<DateTime> GetMaxDateRecordAsync();

        Task<IReadOnlyCollection<WeatherData>> GetWeatherDataAsync(DateTime date);

        Task< IReadOnlyCollection<WeatherData>> GetWeatherDataAsync(DateTime startDate, DateTime endDate);
    }
}
