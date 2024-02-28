namespace WeatherApp.Services
{
    public static class ServicesExtensions
    {
        public static void AddServices(this IServiceCollection serviceCollection)
        {

            serviceCollection.AddSingleton<IWeatherFileReader, InFactoryFileReader>();
            serviceCollection.AddSingleton<IWeatherService, PaginedWeatherService>();
        }
    }
}
