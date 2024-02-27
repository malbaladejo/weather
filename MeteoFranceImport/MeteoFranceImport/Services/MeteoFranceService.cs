using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Web;

namespace MeteoFranceImport.Services
{
	internal class MeteoFranceService : IMeteoFranceService
	{
		private readonly string ApplicationId = "dFFFXzRNdzBwVVlSYlZURnZtemRzTUp4Zk9jYTphRW1CVHRVa2FDV1FIQ0ZoXzEwbWJ0YTNieXNh";
		private string token = "";
		private readonly int departmentMin = 1;
		private readonly int departmentMax = 95;

		private async Task InitializeAsync()
		{
			if (!string.IsNullOrEmpty(this.token))
				return;

			using (var httpClient = new HttpClient())
			{
				httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", ApplicationId);


				var url = "https://portail-api.meteofrance.fr/token";

				var authorizationRequestContent = new FormUrlEncodedContent(new Dictionary<string, string>
				{
					{ "grant_type", "client_credentials" }
				});

				var responseMessage = await httpClient.PostAsync(url, authorizationRequestContent);
				var authorization = await responseMessage.Content.ReadFromJsonAsync<AuthorizationToken>();

				this.token = authorization.Token;
			}
		}

		public async Task<IReadOnlyCollection<Station>> GetStationsAsync(int department)
		{
			await this.InitializeAsync();
			var url = $"https://public-api.meteofrance.fr/public/DPClim/v1/liste-stations/quotidienne?id-departement={department}";

			var httpClient = GetHttpClient();
			try
			{
				var stations = await httpClient.GetFromJsonAsync<IReadOnlyCollection<Station>>(url);

				return stations;
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
				throw;
			}
		}

		public Task<IReadOnlyCollection<StationData>> GetStationDailyDataAsync(string stationId, DateTime beginDate, DateTime endDate)
			=> this.GetStationDataAsync(stationId, "quotidienne", beginDate, endDate);

		public Task<IReadOnlyCollection<StationData>> GetStationHourDataAsync(string stationId, DateTime beginDate, DateTime endDate)
			=> this.GetStationDataAsync(stationId, "horaire", beginDate, endDate);

		private async Task<IReadOnlyCollection<StationData>> GetStationDataAsync(string stationId, string type, DateTime beginDate, DateTime endDate)
		{
			var cacheFolder = "cache/csv";
			var filePath = Path.Combine(cacheFolder, $"{stationId}-{type}-{beginDate.Year}-{beginDate.Month}.csv");
			try
			{
				if (!Directory.Exists(cacheFolder))
					Directory.CreateDirectory(cacheFolder);

				if (!File.Exists(filePath))
				{
					Console.WriteLine($"Load command ID for {stationId} {type} {beginDate.Year}");

					var commandId = await this.GetCommandStationAsync(stationId, type, beginDate, endDate);
					Console.WriteLine($"command ID loaded");

					await this.InitializeAsync();

					Console.WriteLine($"Load command for {stationId} {type} {beginDate.Year}");
					var url = $"https://public-api.meteofrance.fr/public/DPClim/v1/commande/fichier?id-cmde={commandId}";

					var httpClient = GetHttpClient();

					var csv = await httpClient.GetStringAsync(url);
					Console.WriteLine($"File loaded");

					File.WriteAllText(filePath, csv);
				}

				var lines = File.ReadAllLines(filePath);
				var stationDailyData = new List<StationData>();
				Console.WriteLine($"Parse csv");
				for (int i = 1; i < lines.Length; i++)
				{
					var data = lines[i].Split(";");
					try
					{
						stationDailyData.Add(new StationData
						{
							Post = data[0],
							Date = ParseDate(data[1]),
							RainInMm = ParseDouble(data[2])
						});
					}
					catch (Exception e)
					{
						Console.WriteLine(e.Message);
						Console.WriteLine(data);
					}
				}

				return stationDailyData;
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
				throw;
			}
		}

		private static DateTime ParseDate(string value)
		{
			if (value.Length == 8)
				return new DateTime(int.Parse(value.Substring(0, 4)), int.Parse(value.Substring(4, 2)), int.Parse(value.Substring(6, 2)));
			if (value.Length == 10)
				return new DateTime(int.Parse(value.Substring(0, 4)), int.Parse(value.Substring(4, 2)), int.Parse(value.Substring(6, 2)), int.Parse(value.Substring(8, 2)), 0, 0);

			throw new Exception($"Incorrect date format: {value}");
		}

		private static double ParseDouble(string value)
		{
			if (string.IsNullOrEmpty(value))
				return 0;

			if (double.TryParse(value, out var doubleValue))
				return doubleValue;

			throw new Exception($"Incorrect double format: {value}");
		}

		private async Task<string> GetCommandStationAsync(string stationId, string type, DateTime beginDate, DateTime endDate)
		{
			await this.InitializeAsync();
			var url = $"https://public-api.meteofrance.fr/public/DPClim/v1/commande-station/{type}?";

			url += $"id-station={stationId}";
			url += $"&date-deb-periode={SerializeDate(beginDate)}";
			url += $"&date-fin-periode={SerializeDate(endDate)}";

			var httpClient = GetHttpClient();
			try
			{
				var payload = await httpClient.GetFromJsonAsync<CommandStationPayload>(url);

				return payload.Response.Return;
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
				throw;
			}
		}

		private static string SerializeDate(DateTime date) => HttpUtility.UrlEncode(date.ToString("yyyy-MM-ddTHH:mm:ssZ"));

		private HttpClient GetHttpClient()
		{
			var httpClient = new HttpClient();
			httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", this.token);
			return httpClient;
		}
	}
}
