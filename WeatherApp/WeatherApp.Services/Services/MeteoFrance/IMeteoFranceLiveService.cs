namespace WeatherApp.Services
{
    public interface IMeteoFranceLiveService
    {
        Task<string> GetCsvAsync(DateTime beginDate, DateTime endDate);

        Task InitializeAsync();

        Task<string> GetCommandStationAsync(string stationId, string type, DateTime beginDate, DateTime endDate);

        Task<string> LoadCsvFromCommandIdAsync(string commandId);

    }

    public interface IMeteoFranceApiService
    {
        Task<IReadOnlyCollection<Station>> GetStationsAsync(string departmentId);
    }

    public interface IMeteoFranceLiveApiService : IMeteoFranceApiService
    {

    }

    public interface IMeteoFranceFileApiService : IMeteoFranceApiService
    {

    }

    public interface IMeteoFranceMemoryApiService : IMeteoFranceApiService
    {

    }
}