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
            DateTime? previousDate = null;
            DateTime? nextDate = null;

            var beginDate = this.selectedDate;

            if (this.selectedDate < minDate)
                beginDate = minDate;

            if (this.selectedDate > maxDate)
                beginDate = maxDate.BeginOfDay();

            beginDate = new DateTime(beginDate.Year, beginDate.Month, 1);

            if (this.selectedDate > minDate)
                previousDate = beginDate.AddMonths(-1);

            if (this.selectedDate < maxDate)
                nextDate = beginDate.AddMonths(1);

            return new DateContext(beginDate, beginDate.LastDayOfMonth(), previousDate, nextDate, Period.Day);
        }
    }
}
