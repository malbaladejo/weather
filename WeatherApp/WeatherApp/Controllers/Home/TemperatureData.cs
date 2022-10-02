using WeatherApp.Models;

namespace WeatherApp.Controllers.Home
{
    public class TemperatureData
    {
        private readonly WeatherData data;

        public TemperatureData(WeatherData data)
        {
            this.data = data;
        }

        public TimeSpan Date => this.data.Date.TimeOfDay;

        public decimal? OutTemperature => this.data.OutTemperature;

        public decimal? InTemperature => this.data.InTemperature;
    }

    public class WeekTemperatureData
    {

        public WeekTemperatureData(DateTime date, decimal inTemperature, decimal outTemperature)
        {
            Date = date;
            InTemperature = inTemperature;
            OutTemperature = outTemperature;
        }

        public DateTime Date { get; }
        public decimal InTemperature { get; }
        public decimal OutTemperature { get; }
    }
}
