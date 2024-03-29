﻿using WeatherApp.Extensions;
using WeatherApp.ViewModels.DateContexts;

namespace WeatherApp.ViewModels
{
    internal class WeekDateContextFactory : IDateContextFactory
    {
        private readonly DateTime selectedDate;
        //private readonly DateTime minDate;
        //private readonly DateTime maxDate;

        public WeekDateContextFactory(DateTime selectedDate)
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

            beginDate = beginDate.FirstDayOfWeek();
            var endDate = beginDate.LastDayOfWeek();

            //if (beginDate > minDate)
            DateTime previousDate = beginDate.AddDays(-7);

            //if (endDate < maxDate)
            DateTime nextDate = beginDate.AddDays(7);

            return new DateContext(beginDate, endDate, previousDate, nextDate, Period.Week);
        }
    }
}
