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
            var firstDayOfYear = new DateTime(selectedDate.Year, 1, 1);

            var lastDayOfYear = new DateTime(selectedDate.Year, 12, 31);

            DateTime? previousDate = firstDayOfYear.AddYears(-1);
            DateTime? nextDate = firstDayOfYear.AddYears(1);

            if (previousDate < minDate)
                previousDate = null;

            if (nextDate > maxDate)
                nextDate = null;

            return new DateContext(firstDayOfYear, lastDayOfYear, previousDate, nextDate, Period.Year);
        }
    }
}
