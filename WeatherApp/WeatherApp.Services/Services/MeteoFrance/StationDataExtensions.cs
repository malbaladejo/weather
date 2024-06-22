using WeatherApp.Models;

namespace WeatherApp.Services
{
	public static class StationDataExtensions
	{
		public static WeatherData ConvertToWeatherData(this HourStationData data)
			=> new WeatherData
			{
				AbsolutePressure = data.Pressure,
				Date = data.Date,
				Rain = data.RainInMm,
				OutTemperature = data.Temperature
			};
	}
}
