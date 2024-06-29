namespace WeatherApp.Services.Impl.Services.MeteoFrance
{
    internal class DailyStationDataFactory : IStationDataFactory<DailyStationData>
    {
        public DailyStationData Create(string[] data, IReadOnlyDictionary<string, int> headers)
            => new DailyStationData
            {
                Post = data.GetValue<DailyStationData>(headers, nameof(DailyStationData.Post)),
                Date = data.GetDate<DailyStationData>(headers, nameof(DailyStationData.Date)),
                RainInMm = data.GetDecimal<DailyStationData>(headers, nameof(DailyStationData.RainInMm)),
                MinTemperature = data.GetDecimal<DailyStationData>(headers, nameof(DailyStationData.MinTemperature)),
                MaxTemperature = data.GetDecimal<DailyStationData>(headers, nameof(DailyStationData.MaxTemperature))
            };
    }
}
