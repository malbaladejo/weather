using WeatherApp.ViewModels;

namespace WeatherApp.Services
{
    public static class ServicesExtensions
    {
        public static void AddServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IWeatherService, CsvWeatherService>();
        }
    }
}
