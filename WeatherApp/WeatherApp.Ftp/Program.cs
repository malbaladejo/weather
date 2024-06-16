// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using WeatherApp.Ftp;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var host = Host.CreateDefaultBuilder(args)
            .ConfigureServices(services =>
            {
                services.AddScoped<FtpService>();
                services.AddScoped<LocalService>();
                services.AddScoped<ImportMeteoFrance>();

                services.AddLogging(logging =>
                {
                    logging.AddConsole();
                    logging.AddLog4Net();
                });
            })
            .Build();

        var config = WeatherConfiguration.Read();
        await PushFilesThrowFtpAsync(host, config);
        await ImportMeteoFranceDataAsync(host);
    }

    private static async Task PushFilesThrowFtpAsync(IHost host, WeatherConfiguration config)
    {
        foreach (var filePath in SourceFileProvider.GetLastFilesUrl(config.LocalFolder))
        {
            var ftpService = host.Services.GetService<FtpService>();
            await ftpService.UploadFileAsync(filePath, config.User, config.Password, config.RemoteFolder);

            var localService = host.Services.GetService<LocalService>();
            localService.CopyFile(filePath, config.DevFolder);
        }
    }

    private static async Task ImportMeteoFranceDataAsync(IHost host)
    {
        await host.Services.GetService<ImportMeteoFrance>().ImportDataAsync();
    }
}