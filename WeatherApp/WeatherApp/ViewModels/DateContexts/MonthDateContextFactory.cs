using WeatherApp.Extensions;
using WeatherApp.ViewModels.DateContexts;

namespace WeatherApp.ViewModels
{
    internal class MonthDateContextFactory : IDateContextFactory
    {
        private readonly DateTime selectedDate;
        //private readonly DateTime minDate;
        //private readonly DateTime maxDate;

        public MonthDateContextFactory(DateTime selectedDate/*, DateTime minDate, DateTime maxDate*/)
        {
            this.selectedDate = selectedDate;
            //this.minDate = minDate;
            //this.maxDate = maxDate;
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

            beginDate = beginDate.FirstDayOfMonth();
            var endDate = beginDate.LastDayOfMonth();

            //if (beginDate > minDate)
            DateTime previousDate = beginDate.AddMonths(-1);

            // if (endDate < maxDate)
            DateTime nextDate = beginDate.AddMonths(1);

            return new DateContext(beginDate, endDate, previousDate, nextDate, Period.Month);
        }
    }
}
