using WeatherApp.Extensions;
using WeatherApp.ViewModels.DateContexts;

namespace WeatherApp.ViewModels
{
    internal class DayDateContextFactory : IDateContextFactory
    {
        private readonly DateTime selectedDate;
        private readonly DateTime minDate;
        private readonly DateTime maxDate;

        public DayDateContextFactory(DateTime selectedDate, DateTime minDate, DateTime maxDate)
        {
            this.selectedDate = selectedDate;
            this.minDate = minDate;
            this.maxDate = maxDate;
        }

        public DateContext Create()
        {
            DateTime? previousDate = null;
            DateTime? nextDate = null;

            if (selectedDate > minDate)
                previousDate = selectedDate.AddDays(-1);

            if (selectedDate < maxDate)
                nextDate = selectedDate.AddDays(1);

            return new DateContext(selectedDate, selectedDate.EndOfDay(), previousDate, nextDate, Period.Day);
        }
    }
}
