namespace WeatherApp.ViewModels
{
    public static class DateContextExtensions
    {
        public static void AddDateContext(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IDateContextFactoryStrategy, DateContextFactoryStrategy>();
        }
    }
}
