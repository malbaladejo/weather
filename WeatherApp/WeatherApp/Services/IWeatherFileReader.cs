using WeatherApp.Models;

namespace WeatherApp.Services
{
    internal interface IWeatherFileReader
    {
        IEnumerable<WeatherData> Parse(int year, int month);
    }
}