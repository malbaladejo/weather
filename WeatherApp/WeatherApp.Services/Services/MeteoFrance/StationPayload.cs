namespace WeatherApp.Services
{
    public class StationPayload
    {
        public StationPayload(Department department, Station station, IReadOnlyCollection<DailyStationData> data)
        {
            this.Department = department;
            this.Station = station;
            this.Data = data;
        }

        public Department Department { get; }

        public Station Station { get; }

        public IReadOnlyCollection<DailyStationData> Data { get; }
    }
}
