using WeatherApp.Models;

namespace WeatherApp.Services
{
    public interface IWeatherFileReader
    {
        IEnumerable<WeatherData> Parse(int year, int month);
    }
}