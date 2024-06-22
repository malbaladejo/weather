namespace WeatherApp.Services
{
    public interface IMeteoFranceFileReader : IWeatherFileReader
    {
        IEnumerable<HourStationData> ParseCsv(string csv);

        Task<IReadOnlyCollection<HourStationData>> GetLastDataAsync();
    }
}
