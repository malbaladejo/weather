namespace WeatherApp.Services
{
    internal static class CercierConfig
    {
        public static string FolderPath => Path.Combine(MeteoFranceConfig.FolderPath, "cercier");

        public static string FilePath(string year, string month) => Path.Combine(FolderPath, $"cercier-{year}-{month.ToString().PadLeft(2, '0')}.csv");

        public static string FilePath(int year, int month) => FilePath($"{year}", $"{month}");
    }
}
