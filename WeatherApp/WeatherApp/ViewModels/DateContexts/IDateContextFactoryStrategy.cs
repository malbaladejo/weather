namespace WeatherApp.ViewModels
{
    public interface IDateContextFactoryStrategy
    {
        Task<IDateContextFactory> GetFactoryAsync(Period period, DateTime? selectedDate, DateTime? endDate);
    }
}
