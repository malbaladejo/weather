using WeatherApp.Services;
using WeatherApp.ViewModels.DateContexts;

namespace WeatherApp.ViewModels
{
    internal abstract class WeatherDataViewModelBase : IWeatherDataViewModel
    {
        private readonly ControllerActionContext controllerContext;
        private readonly DateContext dateContext;

        protected WeatherDataViewModelBase(ControllerActionContext controllerContext, DateContext dateContext, IWeatherService weatherService)
        {
            this.controllerContext = controllerContext;
            this.dateContext = dateContext;
        }

        public abstract string Title { get; }

        public DateTime BeginDate => dateContext.BeginDate;

        public DateTime? PreviousDate => dateContext.PreviousDate;

        public DateTime? NextDate => dateContext.NextDate;

        public Period Period => dateContext.Period;

        public string PeriodeLabel
        {
            get
            {
                switch (Period)
                {
                    case Period.Day:
                        return "Today";
                    case Period.Week:
                        return "Week";
                    case Period.Month:
                        return "Month";
                    case Period.Year:
                        return "Year";
                    default:
                        throw new InvalidDataException($"{Period} not supported.");
                }
            }
        }

        public string Controller => controllerContext.Controller;

        public string Action => controllerContext.Action;

        public string JsonData { get; protected set; }

        public abstract Task InitializeAsync();
    }
}
