using WeatherApp.Services;
using WeatherApp.ViewModels.DateContexts;

namespace WeatherApp.ViewModels.RainViewModels
{
    internal class RainMonthWeatherDataViewModel : IWeatherDataViewModel
    {
        public RainMonthWeatherDataViewModel()
        {
        }

        public string Title => throw new NotImplementedException();

        public DateTime BeginDate { get; }

        public DateTime? PreviousDate => throw new NotImplementedException();

        public DateTime? NextDate => throw new NotImplementedException();

        public Period Period => throw new NotImplementedException();

        public string PeriodeLabel => throw new NotImplementedException();

        public string Controller => throw new NotImplementedException();

        public string Action => throw new NotImplementedException();

        public string JsonData => throw new NotImplementedException();

        public Task InitializeAsync()
        {
            throw new NotImplementedException();
        }
    }
}
