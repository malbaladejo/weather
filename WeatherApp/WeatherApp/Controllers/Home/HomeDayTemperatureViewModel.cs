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
            this.Data = data.Select(d => new TemperatureData(d)).ToArray();
            this.JsonData = LocalJsonSerializer.Serialize(this.Data);

            if (selectedDate > minDate)
                this.PreviousDate = selectedDate.AddDays(-1);

            if (selectedDate < maxDate)
                this.NextDate = selectedDate.AddDays(1);
        }

        public IReadOnlyCollection<TemperatureData> Data { get;  }
        public string JsonData { get; }
        public DateTime Date { get; }
        public DateTime? PreviousDate { get; }
        public DateTime? NextDate { get; }
    }
}
