namespace WeatherApp.Services
{
	internal interface IMeteoFranceFileReader : IWeatherFileReader
	{
		IEnumerable<StationData> ParseCsv(string csv);

		Task<IReadOnlyCollection<StationData>> GetLastDataAsync();
	}
}
