using System.Net;

namespace WeatherApp.Ftp
{
    internal class FtpService
    {
        public static async Task UploadFileAsync(string localUrl, string user, string password, string remoteFolder)
        {
            var remoteUrl = $"{remoteFolder}/{Path.GetFileName(localUrl)}";

            Console.WriteLine($"Local file: {localUrl}.");
            Console.WriteLine($"Remote url: {remoteUrl}.");

            try
            {
                // Get the object used to communicate with the server.
                var request = (FtpWebRequest)WebRequest.Create(remoteUrl);
                request.Method = WebRequestMethods.Ftp.UploadFile;

                // This example assumes the FTP site uses anonymous logon.
                request.Credentials = new NetworkCredential(user, password);

                // Copy the contents of the file to the request stream.
                await using FileStream fileStream = File.Open(localUrl, FileMode.Open, FileAccess.Read);

                Console.WriteLine($"Openning ftp connection.");
                await using Stream requestStream = request.GetRequestStream();

                Console.WriteLine($"Uploading file.");
                await fileStream.CopyToAsync(requestStream);

                Console.WriteLine($"Upload File Complete.");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
