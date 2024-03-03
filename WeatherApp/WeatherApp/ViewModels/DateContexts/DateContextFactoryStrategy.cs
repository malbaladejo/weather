using WeatherApp.Services;

namespace WeatherApp.ViewModels
{
    internal class DateContextFactoryStrategy : IDateContextFactoryStrategy
    {
        private readonly IWeatherService weatherService;

        public DateContextFactoryStrategy(IWeatherService weatherService)
        {
            this.weatherService = weatherService;
        }

        public async Task<IDateContextFactory> GetFactoryAsync(Period period, DateTime? selectedDate, DateTime? endDate)
        {
            var date = selectedDate.HasValue ? selectedDate.Value : DateTime.Today;

            switch (period)
            {
                case Period.Day:
                    return new DayDateContextFactory(date, endDate);
                case Period.Week:
                    return new WeekDateContextFactory(date, endDate);
                case Period.Month:
                    return new MonthDateContextFactory(date, endDate);
                case Period.Year:
                    return new YearDateContextFactory(date, endDate);
                default:
                    throw new IndexOutOfRangeException($"{period} is not supported.");
            }
        }
    }
}
