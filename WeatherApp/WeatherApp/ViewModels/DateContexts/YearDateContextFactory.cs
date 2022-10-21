using WeatherApp.Extensions;
using WeatherApp.ViewModels.DateContexts;

namespace WeatherApp.ViewModels
{
    internal class YearDateContextFactory : IDateContextFactory
    {
        private readonly DateTime selectedDate;
        private readonly DateTime minDate;
        private readonly DateTime maxDate;

        public YearDateContextFactory(DateTime selectedDate, DateTime minDate, DateTime maxDate)
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

            beginDate = new DateTime(beginDate.Year, 1, 1);
            var endDate = new DateTime(beginDate.Year, 12, 31).EndOfDay();

            if (beginDate > minDate)
                previousDate = new DateTime(beginDate.Year - 1, 1, 1);

            if (endDate < maxDate)
                nextDate = new DateTime(beginDate.Year + 1, 1, 1);

            return new DateContext(beginDate, endDate, previousDate, nextDate, Period.Year);
        }
    }
}
