
namespace WeatherApp.Services
{
    public class StationsPayload
    {
        public StationsPayload(Department department, IReadOnlyCollection<Station> stations)
        {
            this.Department = department;
            this.Stations = stations;
        }

        public Department Department { get; }

        public IReadOnlyCollection<Station> Stations { get; }
    }
}
