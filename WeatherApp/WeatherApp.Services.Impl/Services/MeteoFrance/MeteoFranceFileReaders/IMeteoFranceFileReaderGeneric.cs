namespace WeatherApp.Services.Impl.Services.MeteoFrance
{
    public interface IMeteoFranceFileReaderGeneric<T>
    {
        IEnumerable<T> ParseCsv(string csv);
    }
}
