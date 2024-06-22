namespace WeatherApp.Services
{
    public class Department
    {
        public Department(string code, string name)
        {
            this.Code = code;
            this.Name = name;
        }

        public string Code { get; }

        public string Name { get; }

        public bool StationsLoaded { get; set; }
    }
}
