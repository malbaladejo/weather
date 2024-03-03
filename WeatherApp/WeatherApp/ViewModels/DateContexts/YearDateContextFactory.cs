using WeatherApp.Extensions;
using WeatherApp.ViewModels.DateContexts;

namespace WeatherApp.ViewModels
{
    internal class YearDateContextFactory : IDateContextFactory
    {
        private readonly DateTime selectedDate;
        private readonly DateTime? endDate;

        public YearDateContextFactory(DateTime selectedDate, DateTime? endDate)
        {
            this.selectedDate = selectedDate;
            this.endDate = endDate;
        }

        public DateContext Create()
        {
            var beginDate = this.selectedDate;

            beginDate = new DateTime(beginDate.Year, 1, 1);
            var endDate = this.endDate?.EndOfDay() ?? beginDate.LastDayOfYear();

            var previousDate = new DateTime(beginDate.Year - 1, 1, 1);
            var nextDate = endDate.AddDays(1).BeginOfDay();

            return new DateContext(beginDate, endDate, previousDate, nextDate, Period.Year);
        }
    }
}
