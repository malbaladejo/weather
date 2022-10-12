namespace WeatherApp.ViewModels
{
    public static class WeatherDataViewModelFactoryExtensions
    {
        public static void AddWeatherDataViewModelFactory(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IWeatherDataViewModelFactory, WeatherDataViewModelFactory>();
        }
    }
}
