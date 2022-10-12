namespace WeatherApp.ViewModels
{
    public interface IWeatherDataViewModelFactory
    {
        Task<IWeatherDataViewModel> CreateAsync(ControllerActionContext controllerContext, DateTime? selectedDate, Period period);
    }
}
