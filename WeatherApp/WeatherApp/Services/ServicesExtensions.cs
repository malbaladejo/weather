namespace WeatherApp.Services
{
    public static class ServicesExtensions
    {
        public static void AddServices(this IServiceCollection serviceCollection)
        {

            serviceCollection.AddSingleton<ICsvParser, CsvParser>();
            serviceCollection.AddSingleton<IWeatherService, PaginedCsvWeatherService>();
        }
    }
}
