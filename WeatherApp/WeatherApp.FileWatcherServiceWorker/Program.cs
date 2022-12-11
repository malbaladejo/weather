using WeatherApp.FileWatcherServiceWorker;
using WeatherApp.Ftp;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<Worker>();
        services.AddScoped<FtpService>();
        services.AddScoped<LocalService>();

        services.AddLogging(logging =>
        {
            logging.AddConsole();
            logging.AddLog4Net();
        });
    })
    .Build();

await host.RunAsync();
