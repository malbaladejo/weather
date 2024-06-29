using Microsoft.Extensions.DependencyInjection;

namespace WeatherApp.Services.Impl.Services.MeteoFrance
{
    public static class MeteoFranceFileReaderExtensions
    {
        public static IServiceCollection AddMeteoFranceFileReaders(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IStationDataFactory<HourStationData>, HourStationDataFactory>();
            serviceCollection.AddSingleton<IStationDataFactory<DailyStationData>, DailyStationDataFactory>();

            serviceCollection.AddSingleton<IMeteoFranceFileReaderGeneric<HourStationData>, MeteoFranceFileReaderGeneric<HourStationData>>();
            serviceCollection.AddSingleton<IMeteoFranceFileReaderGeneric<DailyStationData>, MeteoFranceFileReaderGeneric<DailyStationData>>();

            return serviceCollection;
        }

    }
}
