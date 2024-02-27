namespace MeteoFranceImport.Services
{
	internal interface IMeteoFranceService
	{
		Task<IReadOnlyCollection<Station>> GetStationsAsync(int department);
		Task<IReadOnlyCollection<StationData>> GetStationDailyDataAsync(string stationId, DateTime beginDate, DateTime endDate);
	}
}