using Microsoft.Extensions.Logging;

namespace WeatherApp.Ftp
{
    public class LocalService
    {
        private readonly ILogger<LocalService> logger;

        public LocalService(ILogger<LocalService> logger)
        {
            this.logger = logger;
        }

        public void CopyFile(string localUrl, string devFolder)
        {
            var fileName = Path.GetFileName(localUrl);
            var devPath = Path.Combine(devFolder, fileName);
            this.logger.LogInformation($"Copy to {devPath}");
            File.Copy(localUrl, devPath, true);
        }
    }
}
