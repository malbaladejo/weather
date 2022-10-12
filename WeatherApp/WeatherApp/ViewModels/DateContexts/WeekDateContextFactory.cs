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
            var firstDayOfWeek = selectedDate.FirstDayOfWeek();

            DateTime? previousDate = firstDayOfWeek.AddDays(-7); ;
            DateTime? nextDate = firstDayOfWeek.AddDays(7);

            if (previousDate < minDate)
                previousDate = null;

            if (nextDate > maxDate)
                nextDate = null;

            return new DateContext(firstDayOfWeek, firstDayOfWeek.LastDayOfWeek(), previousDate, nextDate, Period.Week);
        }
    }
}
