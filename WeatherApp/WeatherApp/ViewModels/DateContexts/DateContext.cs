using WeatherApp.Extensions;

namespace WeatherApp.ViewModels.DateContexts
{
    public class DateContext
    {
        public DateContext(DateTime beginDate, DateTime endDate, DateTime previousDate, DateTime nextDate, Period period)
        {
            BeginDate = beginDate;
            EndDate = endDate;
            PreviousDate = previousDate;
            NextDate = nextDate;
            Period = period;
            Label = GetPeriodLabel(beginDate, endDate, period);
        }

        public string Label { get; }

        public DateTime BeginDate { get; }

        public DateTime EndDate { get; }

        public DateTime PreviousDate { get; }

        public DateTime NextDate { get; }

        public Period Period { get; }

        private static string GetPeriodLabel(DateTime beginDate, DateTime endDate, Period period)
        {
            switch (period)
            {
                case Period.Day:
                    return LongDate(beginDate);
                case Period.Week:
                    return $"{ShortDate(beginDate)} - {ShortDate(endDate)}";
                case Period.Month:
                    return $"{beginDate.GetMonthName()} {beginDate.ToString("yyyy")}";
                case Period.Year:
                    return beginDate.ToString("yyyy");
                default:
                    throw new ArgumentOutOfRangeException($"{period} not supported");
            }
        }

        public string GetLabel(DateTime date)
        {
            switch (this.Period)
            {
                case Period.Day:
                    return date.TimeOfDay.ToString();
                case Period.Week:
                    return date.ToString(WeekLabel());
                case Period.Month:
                    return date.ToString("dd HH:mm");
                case Period.Year:
                    return $"{date.ToString("dd")} {date.GetMonthName()}";
                default:
                    throw new ArgumentOutOfRangeException($"{this.Period} not supported");
            }
        }

        private static string ShortDate(DateTime date)
            => $"{date.ToString("dd")} {date.GetMonthName()} {date.ToString("yyyy")}";

        private static string LongDate(DateTime date)
            => $"{date.GetDayName()} {date.ToString("dd")} {date.GetMonthName()} {date.ToString("yyyy")}";

        private static string WeekLabel()
        {
            switch (Thread.CurrentThread.CurrentUICulture.NativeName)
            {
                case "en":
                    return "MM/dd HH:mm";
                default:
                    return "dd/MM HH:mm";
            }
        }
    }
}
