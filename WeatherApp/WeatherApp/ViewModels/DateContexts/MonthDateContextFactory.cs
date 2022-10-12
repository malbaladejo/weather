using WeatherApp.Extensions;
using WeatherApp.ViewModels.DateContexts;

namespace WeatherApp.ViewModels
{
    internal class MonthDateContextFactory : IDateContextFactory
    {
        private readonly DateTime selectedDate;
        private readonly DateTime minDate;
        private readonly DateTime maxDate;

        public MonthDateContextFactory(DateTime selectedDate, DateTime minDate, DateTime maxDate)
        {
            this.selectedDate = selectedDate;
            this.minDate = minDate;
            this.maxDate = maxDate;
        }

        public DateContext Create()
        {
            var firstDayOfMonth = selectedDate.FirstDayOfMonth();

            var lastDayOfMonth = selectedDate.LastDayOfMonth();

            DateTime? previousDate = firstDayOfMonth.AddMonths(-1);
            DateTime? nextDate = firstDayOfMonth.AddMonths(1);

            if (previousDate < minDate)
                previousDate = null;

            if (nextDate > maxDate)
                nextDate = null;

            return new DateContext(firstDayOfMonth, lastDayOfMonth, previousDate, nextDate, Period.Month);
        }
    }
}
