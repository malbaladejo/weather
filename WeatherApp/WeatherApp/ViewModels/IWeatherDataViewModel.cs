namespace WeatherApp.ViewModels
{
    public interface IWeatherDataViewModel
    {
        string Title { get; }

        DateTime BeginDate { get; }

        DateTime? PreviousDate { get; }

        DateTime? NextDate { get; }

        Period Period { get; }

        string PeriodeLabel { get; }

        string Controller { get; }

        string Action { get; }

        string JsonData { get; }

        Task InitializeAsync();
    }
}
