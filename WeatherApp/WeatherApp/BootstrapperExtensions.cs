using WeatherApp.Services;
using WeatherApp.ViewModels;

namespace WeatherApp
{
    public static class BootstrapperExtensions
    {
        public static void AddWeatherApp(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddServices();
            serviceCollection.AddDateContext();
            serviceCollection.AddWeatherDataViewModelFactory();
        }
    }
}
