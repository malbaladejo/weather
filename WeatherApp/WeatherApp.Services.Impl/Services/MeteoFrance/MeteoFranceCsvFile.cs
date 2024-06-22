using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;

namespace WeatherApp.Services
{
    internal class MeteoFranceCsvFile
    {
        private readonly IWebHostEnvironment environment;
        private readonly ILogger logger;

        public MeteoFranceCsvFile(
            string year,
            string month,
            IWebHostEnvironment environment,
            ILogger logger)
        {
            this.Year = year;
            this.Month = month;
            this.environment = environment;
            this.logger = logger;
        }

        public string FilePath => Path.Combine(this.environment.WebRootPath, CercierConfig.FilePath(this.Year, this.Month));

        public string Year { get; }

        public string Month { get; }

        public List<string> Lines { get; } = new List<string>();

        public void WriteFile()
        {
            this.EnsureFolder();
            this.logger.LogInformation($"{nameof(MeteoFranceCsvFile)},.WriteFile({this.FilePath})");
            if (File.Exists(this.FilePath))
            {
                this.logger.LogInformation($"{nameof(MeteoFranceCsvFile)}, file {this.FilePath} exists.");
                this.AppendLines();
                return;
            }

            this.logger.LogInformation($"{nameof(MeteoFranceCsvFile)}, file {this.FilePath} does not exist.");
            File.WriteAllText(this.FilePath, string.Join(Environment.NewLine, this.Lines));
        }

        private void AppendLines()
        {
            this.EnsureFolder();
            this.logger.LogInformation($"{nameof(MeteoFranceCsvFile)}, file {this.FilePath}, {this.Lines.Count - 1} lines to write.");
            var appendNewLine = false;
            if (!File.ReadAllText(this.FilePath).EndsWith(Environment.NewLine))
                appendNewLine = true;

            using (var sw = File.AppendText(this.FilePath))
            {
                if (appendNewLine)
                    sw.Write(Environment.NewLine);

                for (int i = 1; i < this.Lines.Count; i++)
                    sw.WriteLine(this.Lines[i]);
            }
        }

        private void EnsureFolder()
        {
            var folderPath = Path.GetDirectoryName(this.FilePath);
            if (Directory.Exists(folderPath))
                return;

            Directory.CreateDirectory(folderPath);
        }
    }
}
