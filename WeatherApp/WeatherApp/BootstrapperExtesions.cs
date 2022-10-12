using WeatherApp.Services;
using WeatherApp.ViewModels;

namespace WeatherApp
{
    public static class BootstrapperExtesions
    {
        public static void AddWeatherApp(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddServices();
            serviceCollection.AddDateContext();
            serviceCollection.AddWeatherDataViewModelFactory();
        }
    }
}
