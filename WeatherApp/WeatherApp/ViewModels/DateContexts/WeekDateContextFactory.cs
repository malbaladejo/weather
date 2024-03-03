using WeatherApp.Extensions;
using WeatherApp.ViewModels.DateContexts;

namespace WeatherApp.ViewModels
{
    internal class WeekDateContextFactory : IDateContextFactory
    {
        private readonly DateTime selectedDate;
        private readonly DateTime? endDate;

        public WeekDateContextFactory(DateTime selectedDate, DateTime? endDate)
        {
            this.selectedDate = selectedDate;
            this.endDate = endDate;
        }

        public DateContext Create()
        {
            var beginDate = this.selectedDate;

            beginDate = beginDate.FirstDayOfWeek();
            var endDate = this.endDate?.EndOfDay() ?? beginDate.LastDayOfWeek();

            var previousDate = beginDate.AddDays(-7);

            var nextDate = endDate.AddDays(1).BeginOfDay();

            return new DateContext(beginDate, endDate, previousDate, nextDate, Period.Week);
        }
    }
}
