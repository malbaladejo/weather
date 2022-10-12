﻿using System.Globalization;

namespace WeatherApp.Extensions
{
    public static class DateExtensions
    {
        public static DateTime BeginOfDay(this DateTime date) => date.Date;

        public static DateTime EndOfDay(this DateTime date) => new DateTime(date.Year, date.Month, date.Day, 23, 59, 59);

        public static DateTime FirstDayOfWeek(this DateTime date)
        {
            while (date.DayOfWeek != CultureInfo.CurrentUICulture.DateTimeFormat.FirstDayOfWeek)
                date = date.AddDays(-1);

            return date.BeginOfDay();
        }

        public static DateTime LastDayOfWeek(this DateTime date)
        {
            var firstDayOfWeek = CultureInfo.CurrentUICulture.DateTimeFormat.FirstDayOfWeek;
            var lasrDayOfWeek = firstDayOfWeek == DayOfWeek.Sunday ? DayOfWeek.Saturday : (DayOfWeek)((int)firstDayOfWeek - 1);
            while (date.DayOfWeek != lasrDayOfWeek)
                date = date.AddDays(1);

            return date.EndOfDay();
        }

        public static DateTime FirstDayOfMonth(this DateTime date)=> new DateTime(date.Year, date.Month, 1);

        public static DateTime LastDayOfMonth(this DateTime date)=>
            new DateTime(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month)).EndOfDay();
    }
}
