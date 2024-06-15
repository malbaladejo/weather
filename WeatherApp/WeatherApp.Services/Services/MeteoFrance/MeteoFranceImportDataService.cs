using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using System.Text.RegularExpressions;
using WeatherApp.Extensions;

namespace WeatherApp.Services
{
    internal class MeteoFranceImportDataService : IMeteoFranceImportDataService
    {
        private readonly IMeteoFranceFileReader meteoFranceFileReader;
        private readonly IMeteoFranceLiveService meteoFranceLiveService;
        private readonly IWebHostEnvironment environment;
        private readonly ILogger<MeteoFranceImportDataService> logger;

        public MeteoFranceImportDataService(
            IMeteoFranceFileReader meteoFranceFileReader,
            IMeteoFranceLiveService meteoFranceLiveService,
            IWebHostEnvironment environment,
            ILogger<MeteoFranceImportDataService> logger)
        {
            this.meteoFranceFileReader = meteoFranceFileReader;
            this.meteoFranceLiveService = meteoFranceLiveService;
            this.environment = environment;
            this.logger = logger;
        }

        public async Task ImportAsync()
        {
            var startDate = new DateTime(2024, 4, 1);

            var data = await this.meteoFranceFileReader.GetLastDataAsync();

            if (data.Count == 0)
                this.logger.LogInformation("No Data");

            var lastImportedData = data.Count > 0 ? data.Max(d => d.Date) : startDate;
            this.logger.LogInformation($"last Imported Data: {lastImportedData}");

            var lastDateToImport = DateTime.Now.BeginOfDay().AddDays(-1);
            this.logger.LogInformation($"last Date To Import: {lastDateToImport}");

            if (lastImportedData >= lastDateToImport)
            {
                this.logger.LogInformation($"Nothing to import.");
                return;
            }

            var firstDateToImport = lastImportedData.AddHours(1);
            this.logger.LogInformation($"first Date To Import: {firstDateToImport}");

            await this.StartImportAsync(firstDateToImport, lastDateToImport);
        }

        private async Task StartImportAsync(DateTime firstDateToImport, DateTime lastDateToImport)
        {
            this.logger.LogInformation($"{nameof(ImportAsync)}: Step 1 - {nameof(this.meteoFranceLiveService.InitializeAsync)}");
            await this.meteoFranceLiveService.InitializeAsync();

            this.logger.LogInformation($"{nameof(ImportAsync)}: Step 2 - {nameof(this.meteoFranceLiveService.GetCommandStationAsync)}");
            var commandId = string.Empty;

            await this.RunAndRetryAsync(async () =>
            {
                commandId = await this.meteoFranceLiveService.GetCommandStationAsync(
                                    meteoFranceLiveService.Cercier,
                                    meteoFranceLiveService.CommandTypeHour,
                                    firstDateToImport,
                                    lastDateToImport);
            });

            this.logger.LogInformation($"{nameof(ImportAsync)}: Step 3 - {nameof(this.meteoFranceLiveService.LoadCsvFromCommandIdAsync)}");
            var csv = string.Empty;
            await this.RunAndRetryAsync(async () =>
            {
                csv = await this.meteoFranceLiveService.LoadCsvFromCommandIdAsync(commandId);
            });

            this.logger.LogInformation($"{nameof(ImportAsync)}: Step 4 - {nameof(this.PrepareFiles)}");
            this.PrepareFiles(csv);
        }

        private async Task RunAndRetryAsync(Func<Task> taskAction)
        {
            const int maxTry = 3;
            const int durationInMs = 3000;
            var nbTry = 0;
            var success = false;

            do
            {
                try
                {
                    await Task.Delay(durationInMs);
                    nbTry++;
                    this.logger.LogInformation($"Try {nbTry} in progress.");
                    await taskAction.Invoke();
                    success = true;
                    this.logger.LogInformation($"Try {nbTry} successed.");
                }
                catch
                {
                    this.logger.LogInformation($"Try {nbTry} in error.");
                }
            } while (!success && nbTry < maxTry);

            if (!success)
                throw new InvalidDataException("Too many try.");
        }

        private void PrepareFiles(string csv)
        {
            var lines = csv.Split(Environment.NewLine);
            this.logger.LogInformation($"{nameof(PrepareFiles)}: file lentgh {csv.Length}.");

            try
            {
                var regex = new Regex("74051002;(?<year>[0-9]{4})(?<month>[0-9]{2})");
                var headers = lines[0];

                lines = lines.Where(l => !string.IsNullOrWhiteSpace(l)).ToArray();

                this.logger.LogInformation($"{nameof(PrepareFiles)}: {lines.Length} lines.");

                MeteoFranceCsvFile csvFile = null;
                for (int i = 1; i < lines.Length; i++)
                {
                    var line = lines[i];
                    var match = regex.Match(line);
                    var year = match.Groups["year"].Value;
                    var month = match.Groups["month"].Value;

                    if (csvFile == null)
                    {
                        this.logger.LogInformation($"New file {year}-{month}.");
                        csvFile = new MeteoFranceCsvFile(year, month, this.environment, this.logger);
                        csvFile.Lines.Add(headers);
                    }

                    if (csvFile.Year != year || csvFile.Month != month)
                    {
                        this.logger.LogInformation($"Write file {csvFile.Year}-{csvFile.Month}.");

                        csvFile.WriteFile();
                        this.logger.LogInformation($"New file {year}-{month}.");
                        csvFile = new MeteoFranceCsvFile(year, month, this.environment, this.logger);
                        csvFile.Lines.Add(headers);
                    }
                    csvFile.Lines.Add(line);
                }

                if (csvFile != null)
                {
                    this.logger.LogInformation($"Write file {csvFile.Year}-{csvFile.Month}.");
                    csvFile.WriteFile();
                }
            }
            catch (Exception e)
            {
                this.logger.LogError($"{nameof(PrepareFiles)}.");
                this.logger.LogError(e.DumpAsString());
            }
        }
    }
}
