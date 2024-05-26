namespace WeatherApp.Services
{
	internal interface IMeteoFranceLiveService
	{
		Task<string> GetCsvAsync(DateTime beginDate, DateTime endDate);

		Task InitializeAsync();

		Task<string> GetCommandStationAsync(string stationId, string type, DateTime beginDate, DateTime endDate);

		Task<string> LoadCsvFromCommandIdAsync(string commandId);

		string Cercier { get; }

		string CommandTypeHour { get; }
	}
}