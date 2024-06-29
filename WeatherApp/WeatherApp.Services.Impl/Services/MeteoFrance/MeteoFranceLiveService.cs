namespace WeatherApp.Services
{

    /// <summary>
    /// https://portail-api.meteofrance.fr/web/fr/
    /// </summary>
    //internal class MeteoFranceLiveService : IMeteoFranceService
    //{
    //    private readonly string ApplicationId = "dFFFXzRNdzBwVVlSYlZURnZtemRzTUp4Zk9jYTphRW1CVHRVa2FDV1FIQ0ZoXzEwbWJ0YTNieXNh";
    //    private readonly IMeteoFranceFileReader meteoFranceFileReader;
    //    private readonly IDepartmentService departmentService;
    //    private readonly IMemoryCache memoryCache;
    //    private readonly ILogger<MeteoFranceLiveService> logger;
    //    private string token = "";
    //    private const string TokenCacheKey = "MeteoFranceToken";

    //    public MeteoFranceLiveService(
    //        IMeteoFranceFileReader meteoFranceFileReader,
    //        IDepartmentService departmentService,
    //        IMemoryCache memoryCache,
    //        ILogger<MeteoFranceLiveService> logger)
    //    {
    //        this.meteoFranceFileReader = meteoFranceFileReader;
    //        this.departmentService = departmentService;
    //        this.memoryCache = memoryCache;
    //        this.logger = logger;
    //    }

    //    private async Task InitializeAsync()
    //    {
    //        if (this.memoryCache.TryGetValue(TokenCacheKey, out this.token))
    //        {
    //            this.LogStep("Token in cache");
    //            return;
    //        }

    //        try
    //        {
    //            using (var httpClient = new HttpClient())
    //            {
    //                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", ApplicationId);

    //                var url = "https://portail-api.meteofrance.fr/token";

    //                var authorizationRequestContent = new FormUrlEncodedContent(new Dictionary<string, string> { { "grant_type", "client_credentials" } });

    //                this.LogStep("Get Token in progress");
    //                var responseMessage = await httpClient.PostAsync(url, authorizationRequestContent);
    //                var authorization = await responseMessage.Content.ReadFromJsonAsync<AuthorizationToken>();

    //                this.token = authorization.Token;
    //                this.LogStep("Get Token OK");

    //                var cacheEntryOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(30));

    //                this.memoryCache.Set(TokenCacheKey, this.token, cacheEntryOptions);
    //            }
    //        }
    //        catch (Exception e)
    //        {
    //            this.logger.LogError(e.DumpAsString());
    //            throw;
    //        }
    //    }

    //    private async Task<string> GetCommandStationAsync(string stationId, string type, DateTime beginDate, DateTime endDate)
    //    {

    //        var url = $"https://public-api.meteofrance.fr/public/DPClim/v1/commande-station/{type}?";

    //        url += $"id-station={stationId}";
    //        url += $"&date-deb-periode={SerializeDate(beginDate)}";
    //        url += $"&date-fin-periode={SerializeDate(endDate)}";

    //        this.logger.LogInformation($"GetCommandStationAsync({stationId}, {type}, {beginDate},{endDate})");

    //        this.logger.LogInformation(url);
    //        try
    //        {
    //            var payload = await this.ReadFromJsonAsync<CommandStationPayload>(url);
    //            this.logger.LogInformation($"command ID loaded");

    //            return payload.Response.Return;
    //        }
    //        catch (Exception e)
    //        {
    //            this.logger.LogError(nameof(GetCommandStationAsync));
    //            this.logger.LogError(e.DumpAsString());
    //            throw;
    //        }
    //    }

    //    private async Task<string> LoadCsvFromCommandIdAsync(string commandId)
    //    {
    //        try
    //        {
    //            this.logger.LogInformation($"Load csv for command {commandId}");
    //            var url = $"https://public-api.meteofrance.fr/public/DPClim/v1/commande/fichier?id-cmde={commandId}";
    //            this.logger.LogInformation(url);

    //            var csv = await this.ReadAsStringAsync(url);
    //            this.logger.LogInformation($"File loaded: csv contains {csv.Length} length.");

    //            if (csv.Length == 0)
    //                throw new InvalidOperationException("The csv is empty.");

    //            return csv;
    //        }
    //        catch (Exception e)
    //        {
    //            this.logger.LogError(nameof(LoadCsvFromCommandIdAsync));
    //            this.logger.LogError(e.DumpAsString());
    //            throw;
    //        }
    //    }

    //    public async Task<string> GetDataAsync(string stationId, string type, DateTime beginDate, DateTime endDate)
    //    {
    //        try
    //        {
    //            this.logger.LogInformation($"GetDataAsync({stationId},{type}, {beginDate}, {endDate})");

    //            await this.InitializeAsync();

    //            var commandId = string.Empty;

    //            await this.RunAndRetryAsync(async () =>
    //            {
    //                commandId = await this.GetCommandStationAsync(
    //                                    MeteoFranceConfig.Cercier,
    //                                    type,
    //                                    beginDate,
    //                                    endDate);
    //            });

    //            var csv = string.Empty;
    //            await this.RunAndRetryAsync(async () =>
    //            {
    //                csv = await this.LoadCsvFromCommandIdAsync(commandId);
    //            });

    //            return csv;
    //        }
    //        catch (Exception e)
    //        {
    //            this.logger.LogError(nameof(LoadCsvFromCommandIdAsync));
    //            this.logger.LogError(e.DumpAsString());
    //            throw;
    //        }
    //    }

    //    public async Task<StationsPayload> GetStationsAsync(string departmentId)
    //    {
    //        this.logger.LogInformation($"{nameof(GetStationsAsync)}({departmentId})");
    //        await this.InitializeAsync();
    //        try
    //        {
    //            StationsPayload payload = null;
    //            await this.RunAndRetryAsync(async () =>
    //            {
    //                payload = await this.GetStationsAsync(departmentId);
    //            });

    //            return payload;
    //        }
    //        catch (Exception e)
    //        {
    //            this.logger.LogError(nameof(LoadCsvFromCommandIdAsync));
    //            this.logger.LogError(e.DumpAsString());
    //            throw;
    //        }
    //    }

    //    private async Task<StationsPayload> GetStationsDataAsync(string departmentId)
    //    {

    //        this.logger.LogInformation($"{nameof(GetStationsDataAsync)}({departmentId})");


    //        var url = $"https://public-api.meteofrance.fr/public/DPClim/v1/liste-stations/quotidienne?id-departement={departmentId}";

    //        logger.LogInformation(url);

    //        try
    //        {
    //            var stations = await ReadFromJsonAsync<IReadOnlyCollection<Station>>(url);
    //            logger.LogInformation($"Number of stations: {stations.Count}");

    //            var openedStations = stations.Where(s => s.PosteOuvert).ToArray();
    //            logger.LogInformation($"Number of opened stations: {openedStations.Length}");
    //            return new StationsPayload(this.departmentService.GetDepartment(departmentId), openedStations);
    //        }
    //        catch (Exception e)
    //        {
    //            logger.LogError(nameof(GetCommandStationAsync));
    //            logger.LogError(e.DumpAsString());
    //            throw;
    //        }
    //    }

    //    private async Task<TReturn> ReadFromJsonAsync<TReturn>(string url)
    //    {
    //        using (var httpClient = GetHttpClient())
    //        {

    //            var result = await httpClient.GetAsync(url);
    //            if (result.IsSuccessStatusCode)
    //            {
    //                return await result.Content.ReadFromJsonAsync<TReturn>();
    //            }
    //            else
    //            {
    //                string content = await result.Content.ReadAsStringAsync();

    //                throw new HttpRequestException(content, null, result.StatusCode);
    //            }
    //        }
    //    }

    //    private async Task<string> ReadAsStringAsync(string url)
    //    {
    //        using (var httpClient = GetHttpClient())
    //        {
    //            var result = await httpClient.GetAsync(url);
    //            if (result.IsSuccessStatusCode)
    //            {
    //                return await result.Content.ReadAsStringAsync();
    //            }
    //            else
    //            {
    //                string content = await result.Content.ReadAsStringAsync();
    //                throw new HttpRequestException(content, null, result.StatusCode);
    //            }
    //        }
    //    }

    //    private static string SerializeDate(DateTime date) => HttpUtility.UrlEncode(date.ToString("yyyy-MM-ddTHH:mm:ssZ"));

    //    private HttpClient GetHttpClient()
    //    {
    //        var httpClient = new HttpClient();
    //        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", this.token);
    //        return httpClient;
    //    }

    //    private void LogStep(string message)
    //    {
    //        this.logger.LogInformation($"Meteo France API: {message}");
    //    }

    //    private async Task RunAndRetryAsync(Func<Task> taskAction)
    //    {
    //        const int maxTry = 3;
    //        const int durationInMs = 3000;
    //        var nbTry = 0;
    //        var success = false;

    //        do
    //        {
    //            try
    //            {
    //                await Task.Delay(durationInMs);
    //                nbTry++;
    //                this.logger.LogInformation($"Try {nbTry} in progress.");
    //                await taskAction.Invoke();
    //                success = true;
    //                this.logger.LogInformation($"Try {nbTry} successed.");
    //            }
    //            catch
    //            {
    //                this.logger.LogInformation($"Try {nbTry} in error.");
    //            }
    //        } while (!success && nbTry < maxTry);

    //        if (!success)
    //            throw new InvalidDataException("Too many try.");
    //    }
    //}
}
