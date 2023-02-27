namespace WeatherApp.ViewModels
{
    public class HomeViewModel
    {
    }

    public class HomeData
    {

        public HomeTemperaturesData Temperature { get; set; }
        public HomeRainData Rain { get; set; }

    }

    public class HomeRainData
    {
        public decimal? Rain { get; set; }
    }

    public class HomeTemperaturesData
    {
        public HomeTemperatureData InData { get; set; }

        public HomeTemperatureData OutData { get; set; }
    }

    public class HomeTemperatureData
    {
        public DateTime MinDate { get; set; }

        public decimal? MinTemperature { get; set; }

        public DateTime MaxDate { get; set; }

        public decimal? MaxTemperature { get; set; }
    }
}
