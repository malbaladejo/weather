using Microsoft.Extensions.Logging;

namespace WeatherApp.Ftp
{
    internal class ImportMeteoFrance
    {
        private readonly ILogger<ImportMeteoFrance> logger;

        public ImportMeteoFrance(ILogger<ImportMeteoFrance> logger)
        {
            this.logger = logger;
        }

        public async Task ImportDataAsync()
        {
            using (var httpClient = new HttpClient())
            {
                try
                {
                    this.logger.LogInformation("Meteo France import start.");
                    await httpClient.GetAsync("https://meteo-copponex.michaelalbaladejo.com/");
                    this.logger.LogInformation("Meteo France import successful.");
                }
                catch (Exception ex)
                {
                    this.logger.LogError("Meteo France import error.");
                    this.logger.LogError(ex.DumpAsString());
                }
                finally
                {
                    this.logger.LogInformation("Meteo France import end.");
                }
            }
        }
    }
}
