namespace WeatherApp.Services
{
    public static class MeteoFranceConfig
    {
        public static string FolderPath => Path.Combine("data", "meteo-france");

        public static string Cercier => "74051002";

        public static string CommandTypeHour => "horaire";

        public static string CommandTypeDaily => "quotidienne";
    }
}