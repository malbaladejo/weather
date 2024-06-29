namespace WeatherApp.Services.Impl.Services.MeteoFrance
{
    public interface IStationDataFactory<T>
    {
        T Create(string[] data, IReadOnlyDictionary<string, int> headers);
    }
}
