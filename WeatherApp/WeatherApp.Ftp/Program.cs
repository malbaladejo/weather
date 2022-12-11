// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using WeatherApp.Ftp;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddScoped<FtpService>();
        services.AddScoped<LocalService>();

        services.AddLogging(logging =>
        {
            logging.AddConsole();
            logging.AddLog4Net();
        });
    })
.Build();

var config = WeatherConfiguration.Read();

foreach (var filePath in SourceFileProvider.GetLastFilesUrl(config.LocalFolder))
{
    var ftpService = host.Services.GetService<FtpService>();
    await ftpService.UploadFileAsync(filePath, config.User, config.Password, config.RemoteFolder);

    var localService = host.Services.GetService<LocalService>();
    localService.CopyFile(filePath, config.DevFolder);
}
