using WeatherApp.Extensions;
using WeatherApp.ViewModels.DateContexts;

namespace WeatherApp.ViewModels
{
    internal class WeekDateContextFactory : IDateContextFactory
    {
        private readonly DateTime selectedDate;
        private readonly DateTime minDate;
        private readonly DateTime maxDate;

        public WeekDateContextFactory(DateTime selectedDate, DateTime minDate, DateTime maxDate)
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

            beginDate = beginDate.FirstDayOfWeek();

            if (this.selectedDate > minDate)
                previousDate = beginDate.AddDays(-7);

            if (this.selectedDate < maxDate)
                nextDate = beginDate.AddDays(7);

            return new DateContext(beginDate, beginDate.LastDayOfWeek(), previousDate, nextDate, Period.Week);
        }
    }
}
