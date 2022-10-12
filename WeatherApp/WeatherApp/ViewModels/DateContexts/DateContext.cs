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
        }

        public DateTime BeginDate { get; }

        public DateTime EndDate { get; }

        public DateTime? PreviousDate { get; }

        public DateTime? NextDate { get; }
        public Period Period { get; }
    }
}
