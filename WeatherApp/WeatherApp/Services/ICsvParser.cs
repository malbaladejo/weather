using WeatherApp.Models;

namespace WeatherApp.Services
{
    internal interface ICsvParser
    {
        IEnumerable<WeatherData> Parse(string inputFile);
    }
}