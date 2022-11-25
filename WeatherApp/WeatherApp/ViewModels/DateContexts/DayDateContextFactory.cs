using WeatherApp.Extensions;
using WeatherApp.ViewModels.DateContexts;

namespace WeatherApp.ViewModels
{
    internal class DayDateContextFactory : IDateContextFactory
    {
        private readonly DateTime selectedDate;
        //private readonly DateTime minDate;
        //private readonly DateTime maxDate;

        public DayDateContextFactory(DateTime selectedDate/*, DateTime minDate, DateTime maxDate*/)
        {
            this.selectedDate = selectedDate.BeginOfDay();
            //this.minDate = minDate.BeginOfDay();
            //this.maxDate = maxDate.EndOfDay();
        }

        public DateContext Create()
        {
            //DateTime? previousDate = null;
            //DateTime? nextDate = null;

            var beginDate = this.selectedDate;

            //if (this.selectedDate < minDate)
            //    beginDate = minDate;

            //if (this.selectedDate > maxDate)
            //    beginDate = maxDate.BeginOfDay();

            //if (this.selectedDate > minDate)
                DateTime previousDate = beginDate.AddDays(-1);

            //  if (this.selectedDate < maxDate)
            DateTime nextDate = beginDate.AddDays(1);

            return new DateContext(beginDate, beginDate.EndOfDay(), previousDate, nextDate, Period.Day);
        }
    }
}
