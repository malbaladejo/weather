namespace WeatherApp.Services
{
	internal interface IMeteoFranceService
	{
		Task<IReadOnlyCollection<StationData>> GetStationHourDataAsync(DateTime beginDate);
		Task<IReadOnlyCollection<StationData>> GetStationHourDataAsync(DateTime beginDate, DateTime endDate);
	}
}