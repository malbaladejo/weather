namespace WeatherApp.Ftp
{
    internal class SourceFileProvider
    {
        public static IEnumerable<string> GetLastFilesUrl(string sourceFolder)
        {
            var directoryInfo = new DirectoryInfo(sourceFolder);

            foreach (var fileInfo in directoryInfo.GetFiles("*.csv"))
            {
                Console.WriteLine($"{fileInfo.Name}: {fileInfo.LastWriteTime}.");
                if (fileInfo.LastWriteTime > DateTime.Now.Date)
                {
                    Console.WriteLine($"{fileInfo.Name} must be upload.");

                    yield return fileInfo.FullName;
                }
            }
        }
    }
}
