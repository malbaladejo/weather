using System.Text.Json;

namespace MeteoFranceImport.Services
{
	internal class MeteoFranceCacheService : IMeteoFranceService
	{
		private readonly MeteoFranceService meteoFranceService;
		private readonly string cacheFolder = "cache";
		public MeteoFranceCacheService(MeteoFranceService meteoFranceService)
		{
			this.meteoFranceService = meteoFranceService;
		}

		public async Task<IReadOnlyCollection<Station>> GetStationsAsync(int department)
		{
			var folder = Path.Combine(this.cacheFolder, "stations");
			var fileName = Path.Combine(folder, $"stations-{department.ToString().PadLeft(2, '0')}.json");

			if (File.Exists(fileName))
			{
				var json = File.ReadAllText(fileName);
				return JsonSerializer.Deserialize<IReadOnlyCollection<Station>>(json);
			}

			var stations = await meteoFranceService.GetStationsAsync(department);

			var result = JsonSerializer.Serialize(stations, new JsonSerializerOptions { WriteIndented = true });
			if (!Directory.Exists(folder))
				Directory.CreateDirectory(folder);

			File.WriteAllText(fileName, result);

			await Task.Delay(2000);
			return stations;
		}

		public async Task<IReadOnlyCollection<StationData>> GetStationDailyDataAsync(string stationId, DateTime beginDate, DateTime endDate)
		{
			var folder = Path.Combine(this.cacheFolder, "daily", beginDate.Year.ToString());
			var fileName = Path.Combine(folder, $"{stationId}-{beginDate.Year}.json");

			if (File.Exists(fileName))
			{
				var json = File.ReadAllText(fileName);
				return JsonSerializer.Deserialize<IReadOnlyCollection<StationData>>(json);
			}

			var data = await meteoFranceService.GetStationDailyDataAsync(stationId, beginDate, endDate);

			var result = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
			if (!Directory.Exists(folder))
				Directory.CreateDirectory(folder);

			File.WriteAllText(fileName, result);

			await Task.Delay(2000);
			return data;
		}

		public async Task<IReadOnlyCollection<StationData>> GetStationHourDataAsync(string stationId, DateTime beginDate, DateTime endDate)
		{
			var folder = Path.Combine(this.cacheFolder, "hour", beginDate.Year.ToString());
			var fileName = Path.Combine(folder, $"{stationId}-{beginDate.Year}-{beginDate.Month}.json");

			if (File.Exists(fileName))
			{
				var json = File.ReadAllText(fileName);
				return JsonSerializer.Deserialize<IReadOnlyCollection<StationData>>(json);
			}

			var data = await meteoFranceService.GetStationHourDataAsync(stationId, beginDate, endDate);

			var result = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
			if (!Directory.Exists(folder))
				Directory.CreateDirectory(folder);

			File.WriteAllText(fileName, result);

			await Task.Delay(2000);
			return data;
		}
	}
}
