namespace WeatherApp.Ftp
{
    internal class LocalService
    {
        public static void CopyFile(string localUrl, string devFolder)
        {
            var fileName = Path.GetFileName(localUrl);
            var devPath = Path.Combine(devFolder, fileName);
            Console.WriteLine($"Copy to {devPath}");
            File.Copy(localUrl, devPath, true);
        }
    }
}
