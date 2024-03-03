using WeatherApp.Extensions;
using WeatherApp.ViewModels.DateContexts;

namespace WeatherApp.ViewModels
{
    internal class DayDateContextFactory : IDateContextFactory
    {
        private readonly DateTime selectedDate;
        private readonly DateTime? endDate;

        public DayDateContextFactory(DateTime selectedDate, DateTime? endDate)
        {
            this.selectedDate = selectedDate.BeginOfDay();
            this.endDate = endDate;
        }

        public DateContext Create()
        {
            var beginDate = this.selectedDate;
            var endDate = this.endDate?.EndOfDay() ?? beginDate.EndOfDay();
            var previousDate = beginDate.AddDays(-1);
            var nextDate = endDate.BeginOfDay().AddDays(1);

            return new DateContext(beginDate, endDate, previousDate, nextDate, Period.Day);
        }
    }
}
