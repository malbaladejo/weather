using MeteoFranceImport.Services;
using System.Diagnostics;
using System.Text.Json;
using System.Text.RegularExpressions;

internal class Program
{
	private const string columnName = "Rain Hour(mm)";

	private async static Task Main(string[] args)
	{
		//var service = new MeteoFranceCacheService(new MeteoFranceService());
		//await LoadDataAsync(service, "74051002", 2023);
		//await LoadStationsDataAsync(service);

		//await service.GetStationHourDataAsync("74051002", new DateTime(2023, 10, 1), new DateTime(2023, 10, 31, 23, 59, 59));
		//await service.GetStationHourDataAsync("74051002", new DateTime(2023, 11, 1), new DateTime(2023, 11, 30, 23, 59, 59));
		//await service.GetStationHourDataAsync("74051002", new DateTime(2023, 12, 1), new DateTime(2023, 12, 31, 23, 59, 59));
		//await service.GetStationHourDataAsync("74051002", new DateTime(2024, 1, 1), new DateTime(2024, 1, 31, 23, 59, 59));
		//await service.GetStationHourDataAsync("74051002", new DateTime(2024, 2, 1), new DateTime(2024, 2, 27, 23, 59, 59));

		FixInFactoryFilesBasedOnMeteoFranceFiles();
	}

	private static void FixInFactoryFilesBasedOnMeteoFranceFiles()
	{
		const string inFactory = "D:\\dev\\Weather\\MeteoFranceImport\\MeteoFranceImport\\bin\\Debug\\net6.0\\infactory\\target";
		const string inFactoryFixed = "D:\\dev\\Weather\\MeteoFranceImport\\MeteoFranceImport\\bin\\Debug\\net6.0\\infactory\\fixed";
		var regex = new Regex("weather-(?<year>[0-9]{4})-(?<month>[0-9]{2}).csv");

		foreach (var filePath in Directory.GetFiles(inFactory))
		{
			var fileName = Path.GetFileName(filePath);
			Console.WriteLine(fileName);

			var targetFilePath = Path.Combine(inFactoryFixed, fileName);
			var match = regex.Match(fileName);

			var year = match.Groups["year"].Value;
			var month = match.Groups["month"].Value;

			Console.WriteLine($"{year}-{month}");

			var meteoFranceData = GetMeteoFranceData(year, month);

			var inFactoryCsvLines = File.ReadAllLines(filePath);

			var rainIndex = Array.IndexOf(inFactoryCsvLines[0].Split("\t"), columnName);
			for (int i = 1; i < inFactoryCsvLines.Length; i++)
			{
				var line = inFactoryCsvLines[i].Split("\t");
				var date = DateTime.Parse(line[1]);
				if (date.Minute > 30)
					continue;

				if (line[rainIndex] != "0.0" && !string.IsNullOrEmpty(line[rainIndex]))
				{
					Console.WriteLine($"{date}: not 0.0");
					continue;
				}

				var hourMeteoFranceData = meteoFranceData.FirstOrDefault(d => d.Date.Day == date.Day && d.Date.Hour == date.Hour);
				if (hourMeteoFranceData == null)
				{
					Console.WriteLine($"{date}: meteo france not found");
					continue;
				}

				if (hourMeteoFranceData.RainInMm == 0)
				{
					Console.WriteLine($"{date}: meteo france 0");
					continue;
				}

				Console.WriteLine($"{date}: {hourMeteoFranceData.RainInMm}");

				line[rainIndex] = hourMeteoFranceData.RainInMm.ToString().Replace(",", ".");
				inFactoryCsvLines[i] = string.Join("\t", line);
			}

			File.WriteAllLines(Path.Combine(inFactoryFixed, fileName), inFactoryCsvLines);
			Console.WriteLine($"{fileName} OK");
		}
	}

	private static IReadOnlyCollection<StationData> GetMeteoFranceData(string year, string month)
	{
		const string meteoFranceSource = "D:\\dev\\Weather\\MeteoFranceImport\\MeteoFranceImport\\bin\\Debug\\net6.0\\cache\\hour";
		const string Cercier = "74051002";

		var meteoFranceJsonPath = Path.Combine(meteoFranceSource, year, $"{Cercier}-{year}-{month}.json");


		Console.WriteLine($"Meteofrance {Path.GetFileName(meteoFranceJsonPath)}");

		var meteoFranceJson = File.ReadAllText(meteoFranceJsonPath);
		return JsonSerializer.Deserialize<IReadOnlyCollection<StationData>>(meteoFranceJson);
	}

	// Parcours tous les fichiers csv InFactory et ajoute la colonne "Rain Hour(mm)" si absente
	private static void FixInFactory()
	{

		const string inFactory = "D:\\dev\\Weather\\MeteoFranceImport\\MeteoFranceImport\\bin\\Debug\\net6.0\\infactory";
		var source = Path.Combine(inFactory, "source");
		var target = Path.Combine(inFactory, "target");

		foreach (var filePath in Directory.GetFiles(source))
		{
			var fileName = Path.GetFileName(filePath);
			var targetFilePath = Path.Combine(target, fileName);
			Console.WriteLine(fileName);

			var lines = File.ReadAllLines(filePath);

			if (lines[0].Contains(columnName))
			{
				Console.WriteLine("File OK");
				File.Copy(filePath, targetFilePath);
				continue;
			}

			Console.WriteLine($"Column [{columnName}] missing");

			lines[0] = $"{lines[0]}\t{columnName}";
			for (int i = 1; i < lines.Length; i++)
			{
				lines[i] = $"{lines[i]}\t0.0";
			}

			Console.WriteLine($"Writting [{fileName}]");
			File.WriteAllLines(targetFilePath, lines);
		}
	}


	private static async Task LoadDataAsync(IMeteoFranceService service, string stationId, int year)
	{
		await service.GetStationDailyDataAsync(stationId, new DateTime(year, 1, 1), new DateTime(year, 12, 31, 23, 59, 59));
	}

	private static async Task LoadStationsDataAsync(IMeteoFranceService service)
	{
		var globalStopwatch = new Stopwatch();
		var stopwatch = new Stopwatch();

		globalStopwatch.Start();
		for (int i = 1; i <= 95; i++)
		{
			stopwatch.Start();
			Console.WriteLine($"Loading {i}");
			var stations = await service.GetStationsAsync(i);
			await LoadDataAsync(service, stations.First(s => s.PosteOuvert).Id, 2023);
			Console.WriteLine($"{stations.Count} stations loded in {stopwatch.ElapsedMilliseconds}ms");
			stopwatch.Stop();
			stopwatch.Reset();
		}

		Console.WriteLine($"All stations loaded in {globalStopwatch.Elapsed}ms");
		globalStopwatch.Stop();
	}

	private static async Task LoadAllStationsAsync(IMeteoFranceService service)
	{
		var globalStopwatch = new Stopwatch();
		var stopwatch = new Stopwatch();

		globalStopwatch.Start();
		for (int i = 1; i <= 95; i++)
		{
			stopwatch.Start();
			Console.WriteLine($"Loading {i}");
			var stations = await service.GetStationsAsync(i);
			Console.WriteLine($"{stations.Count} stations loded in {stopwatch.ElapsedMilliseconds}ms");
			stopwatch.Stop();
			stopwatch.Reset();
		}

		Console.WriteLine($"All stations loaded in {globalStopwatch.Elapsed}ms");
		globalStopwatch.Stop();
	}
}