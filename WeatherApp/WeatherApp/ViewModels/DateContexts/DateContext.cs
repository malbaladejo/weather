namespace WeatherApp.ViewModels.DateContexts
{
    public class DateContext
    {
        public DateContext(DateTime beginDate, DateTime endDate, DateTime? previousDate, DateTime? nextDate, Period period)
        {
            BeginDate = beginDate;
            EndDate = endDate;
            PreviousDate = previousDate;
            NextDate = nextDate;
            Period = period;
            Label = GetLabel(beginDate, endDate, period);
        }

        public string Label { get; }

        public DateTime BeginDate { get; }

        public DateTime EndDate { get; }

        public DateTime? PreviousDate { get; }

        public DateTime? NextDate { get; }

        public Period Period { get; }

        private static string GetLabel(DateTime beginDate, DateTime endDate, Period period)
        {
            switch (period)
            {
                case Period.Day:
                    return beginDate.ToString("dddd dd MMMM yyyy");
                case Period.Week:
                    return $"{beginDate.ToShortDateString()} - {endDate.ToShortDateString()}";
                case Period.Month:
                    return beginDate.ToString("MMMM yyyy");
                case Period.Year:
                   return beginDate.ToString("yyyy");
                default:
                    throw new ArgumentOutOfRangeException($"{period} not supported");
            }
        }
    }
}
