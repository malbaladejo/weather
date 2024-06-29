using Microsoft.Extensions.DependencyInjection;
using WeatherApp.Services.Impl.Services.MeteoFrance;

namespace WeatherApp.Services
{
    public static class WeatherServicesExtensions
    {
        public static IServiceCollection AddWeatherServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddMeteoFranceServices();

            serviceCollection.AddSingleton<IAggreagatedWeatherService, AggreagatedWeatherService>();
            serviceCollection.AddSingleton<IWeatherService, CachedWeatherService>();

            serviceCollection.AddSingleton<IWeatherFileReader, InFactoryFileReader>();



            return serviceCollection;
        }
    }
}
