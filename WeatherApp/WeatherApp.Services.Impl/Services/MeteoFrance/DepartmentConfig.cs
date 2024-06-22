namespace WeatherApp.Services
{
    internal static class DepartmentConfig
    {
        public static string FolderPath => Path.Combine(MeteoFranceConfig.FolderPath, "departments");
    }
}
