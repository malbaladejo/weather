using Microsoft.Extensions.Logging;
using System.Net;

namespace WeatherApp.Ftp
{
    public class FtpService
    {
        private readonly ILogger<FtpService> logger;

        public FtpService(ILogger<FtpService> logger)
        {
            this.logger = logger;
        }

        public async Task UploadFileAsync(string localUrl, string user, string password, string remoteFolder)
        {
            var remoteUrl = $"{remoteFolder}/{Path.GetFileName(localUrl)}";

            this.logger.LogInformation($"Local file: {localUrl}.");
            this.logger.LogInformation($"Remote url: {remoteUrl}.");

            try
            {
                // Get the object used to communicate with the server.
                var request = (FtpWebRequest)WebRequest.Create(remoteUrl);
                request.Method = WebRequestMethods.Ftp.UploadFile;

                // This example assumes the FTP site uses anonymous logon.
                request.Credentials = new NetworkCredential(user, password);

                // Copy the contents of the file to the request stream.
                await using FileStream fileStream = File.Open(localUrl, FileMode.Open, FileAccess.Read);

                this.logger.LogInformation($"Openning ftp connection.");
                await using Stream requestStream = request.GetRequestStream();

                this.logger.LogInformation($"Uploading file.");
                await fileStream.CopyToAsync(requestStream);

                this.logger.LogInformation($"Upload File Complete.");
            }
            catch (Exception e)
            {
                this.logger.LogInformation(e.Message);
            }
        }
    }
}
