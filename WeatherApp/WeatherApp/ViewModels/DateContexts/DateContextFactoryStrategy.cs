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

        public async Task<IDateContextFactory> GetFactoryAsync(Period period, DateTime? selectedDate)
        {
            var minDate = await this.weatherService.GetMinDateRecordAsync();
            var maxDate = await this.weatherService.GetMaxDateRecordAsync();
            var date = selectedDate.HasValue ? selectedDate.Value : maxDate;

            switch (period)
            {
                case Period.Day:
                    return new DayDateContextFactory(date, minDate, maxDate);
                case Period.Week:
                    return new WeekDateContextFactory(date, minDate, maxDate);
                case Period.Month:
                    return new MonthDateContextFactory(date, minDate, maxDate);
                case Period.Year:
                    return new YearDateContextFactory(date, minDate, maxDate);
                default:
                    throw new IndexOutOfRangeException($"{period} is not supported.");
            }
        }
    }
}
