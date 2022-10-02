using System.Text.Json;
using WeatherApp.JsonConverters;
using WeatherApp.Models;

namespace WeatherApp.Controllers.Home
{
    public class HomeDayTemperatureViewModel
    {
        public HomeDayTemperatureViewModel(DateTime minDate, DateTime maxDate, DateTime selectedDate, IReadOnlyCollection<WeatherData> data)
        {
            this.Date = selectedDate;
            var dayData = data.Select(d => new TemperatureData(d)).ToArray();
            this.JsonData = LocalJsonSerializer.Serialize(dayData);

            if (selectedDate > minDate)
                this.PreviousDate = selectedDate.AddDays(-1);

            if (selectedDate < maxDate)
                this.NextDate = selectedDate.AddDays(1);
        }

        public string JsonData { get; }
        public DateTime Date { get; }
        public DateTime? PreviousDate { get; }
        public DateTime? NextDate { get; }
    }
}
