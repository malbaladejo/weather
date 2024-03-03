namespace WeatherApp.ViewModels
{
    public interface IWeatherDataViewModelFactory
    {
        Task<IWeatherDataViewModel> CreateAsync(ControllerActionContext controllerContext, Period period, DateTime? selectedDate, DateTime? endDate = null);
    }
}
