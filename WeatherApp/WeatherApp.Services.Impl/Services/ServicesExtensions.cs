﻿using Microsoft.Extensions.DependencyInjection;

namespace WeatherApp.Services
{
    public static class ServicesExtensions
    {
        public static void AddServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IAggreagatedWeatherService, AggreagatedWeatherService>();
            serviceCollection.AddSingleton<IWeatherService, CachedWeatherService>();

            serviceCollection.AddSingleton<IWeatherFileReader, InFactoryFileReader>();

            serviceCollection.AddSingleton<IWeatherFileReader, MeteoFranceFileReader>();
            serviceCollection.AddSingleton<IMeteoFranceFileReader, MeteoFranceFileReader>();
            serviceCollection.AddSingleton<IMeteoFranceLiveService, MeteoFranceLiveService>();

            serviceCollection.AddSingleton<IMeteoFranceApiService, MeteoFranceFileApiService>();
            serviceCollection.AddSingleton<IMeteoFranceLiveApiService, MeteoFranceLiveService>();
            serviceCollection.AddSingleton<IMeteoFranceFileApiService, MeteoFranceFileApiService>();

            serviceCollection.AddSingleton<IMeteoFranceImportDataService, MeteoFranceImportDataService>();
            serviceCollection.AddSingleton<IDepartmentService, DepartmentService>();
        }
    }
}