namespace WeatherApp.Services
{
    public interface IDepartmentService
    {
        string DepartmentFilePath(string code);

        Department GetDepartment(string code);

        IReadOnlyCollection<Department> GetDepartments();

        void RefreshDepartmentsStatus();
    }
}