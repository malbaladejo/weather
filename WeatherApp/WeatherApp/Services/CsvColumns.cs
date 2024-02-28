namespace WeatherApp.Services
{
    internal static class CsvColumns
    {
        public const string LineNmber = "No.";
        public const string Time = "Time";
        public const string InTemp = "In Temp.(°C)";
        public const string InHumi = "In Humi.(%)";
        public const string Temp = "CH1 Temp.(°C)";
        public const string Humi = "CH1 Humi.(%)";
        public const string Dew = "CH1 Dew(°C)";
        public const string Feel = "CH1 Feel(°C)";
        public const string Wind = "Wind(m/s)";
        public const string Gust = "Gust(m/s) ";
        public const string WindDirection = "Wind Direction";
        public const string ABSPressure = "ABS Pressure(hpa)";
        public const string RELPressure = "REL Pressure(hpa)";
        public const string RainHour = "Rain Hour(mm)";
        public const string RainDay = "Rain Day(mm)";
        public const string RainWeek = "Rain Week(mm)";
        public const string RainMonth = "Rain Month(mm)";
        public const string RainTotal = "Rain Total(mm)";

        public static readonly string[] ColumnsToRead = new[]
        {
            Time, Humi, InHumi, Temp, InTemp, RainHour, Wind, WindDirection, ABSPressure, RELPressure
        };
    }
}
