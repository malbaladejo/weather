namespace WeatherApp.Services
{
    public interface IMeteoFranceFileApiService
    {
        Task<StationsPayload> GetStationsAsync(string departmentId);

        Task<StationPayload> GetStationDataAsync(string stationId, int year);
    }
}