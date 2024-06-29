namespace WeatherApp.Services
{
    public interface IDepartmentService
    {
        string DepartmentFilePath(string code);

        Department GetDepartment(string code);

        Department GetDepartmentFromStationId(string stationId);

        IReadOnlyCollection<Department> GetDepartments();

        void RefreshDepartmentsStatus();
    }
}