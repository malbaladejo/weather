namespace WeatherApp.Services.Impl.Services.MeteoFrance
{
    internal class HourStationDataFactory : IStationDataFactory<HourStationData>
    {
        public HourStationData Create(string[] data, IReadOnlyDictionary<string, int> headers)
            => new HourStationData
            {
                Post = data.GetValue<HourStationData>(headers, nameof(HourStationData.Post)),
                Date = data.GetDate<HourStationData>(headers, nameof(HourStationData.Date)),
                RainInMm = data.GetDecimal<HourStationData>(headers, nameof(HourStationData.RainInMm)),
                Temperature = data.GetDecimal<HourStationData>(headers, nameof(HourStationData.Temperature)),
                Pressure = data.GetDecimal<HourStationData>(headers, nameof(HourStationData.Pressure))
            };
    }
}
