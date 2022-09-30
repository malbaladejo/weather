using System;
using System.Globalization;

namespace WeatherImport
{
    public static class DateExtensions
    {
        public static DateTime BeginOfDay(this DateTime date) => date.Date;

        public static DateTime EndOfDay(this DateTime date) => new DateTime(date.Year, date.Month, date.Day, 23, 59, 59);

        public static DateTime FirstDayOfWeek(this DateTime date)
        {
            while (date.DayOfWeek != CultureInfo.CurrentUICulture.DateTimeFormat.FirstDayOfWeek)
                date = date.AddDays(-1);

            return date;
        }

        public static DateTime LastDayOfWeek(this DateTime date)
        {
            var firstDayOfWeek = CultureInfo.CurrentUICulture.DateTimeFormat.FirstDayOfWeek;
            var lasrDayOfWeek = firstDayOfWeek == DayOfWeek.Sunday ? DayOfWeek.Saturday : (DayOfWeek)((int)firstDayOfWeek - 1);
            while (date.DayOfWeek != lasrDayOfWeek)
                date = date.AddDays(1);

            return date;
        }
    }
}
