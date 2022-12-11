using WeatherApp.Ftp;

namespace WeatherApp.FileWatcherServiceWorker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> logger;
        private readonly FtpService ftpService;
        private readonly LocalService localService;
        private FileSystemWatcher fileSystemWatcher;
        private readonly WeatherConfiguration config;
        private CancellationTokenSource uploadCancellationTokenSource;

        public Worker(ILogger<Worker> logger, FtpService ftpService, LocalService localService)
        {
            this.logger = logger;
            this.ftpService = ftpService;
            this.localService = localService;
            this.config = WeatherConfiguration.Read();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            this.logger.LogInformation($"Start servcice.");
            this.StartFileSystemWatchers();
            //while (!stoppingToken.IsCancellationRequested)
            //{
            //    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            //    await Task.Delay(1000, stoppingToken);
            //}
        }

        /// <summary>Start the file system watcher for each of the file
        /// specification and folders found on the List<>/// </summary>
        private void StartFileSystemWatchers()
        {

            try
            {
                // Record a log entry into Windows Event Log
                this.logger.LogInformation($"{config.LocalFolder}.");

                var dir = new DirectoryInfo(config.LocalFolder);

                // Checks whether the folder is enabled and
                // also the directory is a valid location
                if (!dir.Exists)
                {
                    this.logger.LogWarning($"{config.LocalFolder} does not exist.");
                    return;
                }

                // Creates a new instance of FileSystemWatcher
                var fileSWatch = new FileSystemWatcher();

                // Sets the filter
                fileSWatch.Filter = "*.csv";

                // Sets the folder location
                fileSWatch.Path = config.LocalFolder;

                // Subscribe to notify filters
                fileSWatch.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.DirectoryName;

                fileSWatch.Changed += this.OnFileChanged;

                // Begin watching
                fileSWatch.EnableRaisingEvents = true;

                // Add the systemWatcher to the list
                this.fileSystemWatcher = fileSWatch;

                // Record a log entry into Windows Event Log
                this.logger.LogInformation($"Starting to monitor files with extension ({fileSWatch.Filter}) in the folder ({fileSWatch.Path})");

            }
            catch (Exception e)
            {
                this.logger.LogError(e.Message);
            }
        }

        private void StopFileSystemWatchers()
        {
            if (fileSystemWatcher != null)
            {
                // Stop listening
                fileSystemWatcher.EnableRaisingEvents = false;
                fileSystemWatcher.Changed -= this.OnFileChanged;
                // Dispose the Object
                fileSystemWatcher.Dispose();
            }
        }

        private async void OnFileChanged(object sender, FileSystemEventArgs e)
        {
            var fileName = e.FullPath;
            this.logger.LogInformation($"Changed: {fileName}");
            await this.UploadFileAsync(fileName);
        }

        private async Task UploadFileAsync(string filePath)
        {
            try
            {
                if (this.uploadCancellationTokenSource != null)
                    this.uploadCancellationTokenSource.Cancel();

                this.uploadCancellationTokenSource = new CancellationTokenSource();

                await this.UploadFileAsync(filePath, this.uploadCancellationTokenSource.Token);
            }
            catch (Exception e)
            {
                this.logger.LogError(e.Message);
            }
        }

        private async Task UploadFileAsync(string filePath, CancellationToken cancellationToken)
        {
            await Task.Delay(1000);
            if (cancellationToken.IsCancellationRequested)
                return;

            this.logger.LogInformation($"Processing {filePath}.");


            await this.ftpService.UploadFileAsync(filePath, config.User, config.Password, config.RemoteFolder);
            this.localService.CopyFile(filePath, config.DevFolder);

            this.logger.LogInformation($"Processed {filePath}.");
        }
    }
}