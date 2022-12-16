using WeatherApp.Models;

namespace WeatherApp.Services
{
    public static class WeatherDataColectionExtensions
    {
        public static IEnumerable<WeatherData> Filter(
            this IReadOnlyCollection<WeatherData> data, Func<WeatherData, bool> hasDataFilter)
        {
            return data
                .Where(hasDataFilter)
                .OrderBy(d => d.Date);
        }

        public static bool FilterTemperature(this WeatherData d) => d.InTemperature.HasValue && d.OutTemperature.HasValue;

        public static bool FilterHumidity(this WeatherData d) => d.InHumidity.HasValue && d.OutHumidity.HasValue;

        public static bool FilterPressure(this WeatherData d) => d.RelativePressure.HasValue && d.AbsolutePressure.HasValue;

        public static bool FilterWind(this WeatherData d) => d.Wind.HasValue;
    }
}
