using WeatherApp.Extensions;
using WeatherApp.ViewModels.DateContexts;

namespace WeatherApp.ViewModels
{
    internal class MonthDateContextFactory : IDateContextFactory
    {
        private readonly DateTime selectedDate;
        private readonly DateTime? endDate;

        public MonthDateContextFactory(DateTime selectedDate, DateTime? endDate)
        {
            this.selectedDate = selectedDate;
            this.endDate = endDate;
        }

        public DateContext Create()
        {
            var beginDate = this.selectedDate;

            beginDate = new DateTime(beginDate.Year, beginDate.Month, 1);
            var endDate = this.endDate?.EndOfDay() ?? beginDate.LastDayOfMonth();

            var previousDate = beginDate.AddMonths(-1);
            var nextDate = endDate.AddDays(1).BeginOfDay();

            return new DateContext(beginDate, endDate, previousDate, nextDate, Period.Month);
        }
    }
}
