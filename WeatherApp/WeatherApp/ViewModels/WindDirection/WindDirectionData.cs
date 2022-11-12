namespace WeatherApp.ViewModels
{
    public class WindDirectionData
    {
        public WindDirectionData(string direction, decimal value)
        {
            Direction = direction;
            Value = value;
        }

        public string Direction { get; }
        public decimal Value { get; }
    }
}
