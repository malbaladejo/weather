using Microsoft.Extensions.DependencyInjection;

namespace WeatherApp.Services.Impl.Services.MeteoFrance
{
    public static class MeteoFranceServicesExtensions
    {
        public static IServiceCollection AddMeteoFranceServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddMeteoFranceFileReaders();


            serviceCollection.AddSingleton<IWeatherFileReader, MeteoFranceFileReader>();
            serviceCollection.AddSingleton<IMeteoFranceFileReader, MeteoFranceFileReader>();

            serviceCollection.AddSingleton<IMeteoFranceLiveApiService, MeteoFranceLiveApiService>();
            serviceCollection.AddSingleton<IMeteoFranceFileApiService, MeteoFranceFileApiService>();

            serviceCollection.AddSingleton<IMeteoFranceImportDataService, MeteoFranceImportDataService>();
            serviceCollection.AddSingleton<IDepartmentService, DepartmentService>();

            return serviceCollection;
        }
    }
}
