namespace WeatherApp.Services
{
    public interface IMeteoFranceService
    {
        Task<IReadOnlyCollection<HourStationData>> GetStationHourDataAsync(DateTime beginDate);
        Task<IReadOnlyCollection<HourStationData>> GetStationHourDataAsync(DateTime beginDate, DateTime endDate);
    }
}