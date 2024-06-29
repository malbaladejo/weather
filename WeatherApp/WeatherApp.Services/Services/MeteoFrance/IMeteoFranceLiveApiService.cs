namespace WeatherApp.Services
{
    public interface IMeteoFranceLiveApiService
    {
        Task<IReadOnlyCollection<Station>> GetStationsAsync(string departmentId);


        Task<string> GetStationDataAsync(string stationId, string type, DateTime beginDate, DateTime endDate);
    }
}