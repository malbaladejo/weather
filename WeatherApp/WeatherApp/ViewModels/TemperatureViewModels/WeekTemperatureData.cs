namespace WeatherApp.ViewModels.Temperature
{
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
