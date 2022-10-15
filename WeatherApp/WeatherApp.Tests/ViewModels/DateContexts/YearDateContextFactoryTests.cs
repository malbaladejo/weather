using WeatherApp.Extensions;

namespace WeatherApp.ViewModels.Tests
{
    [TestClass()]
    public class YearDateContextFactoryTests
    {
        [TestMethod()]
        public void YearDateContextFactory_Selected_Date_In_Range_Test()
        {
            // Arrange
            var selectedDate = new DateTime(2022, 7, 8);
            var minDate = new DateTime(2020, 5, 20);
            var maxDate = new DateTime(2024, 10, 9);
            var factory = new YearDateContextFactory(selectedDate.AddHours(5), minDate, maxDate);

            // Act
            var dateContext = factory.Create();

            // Assert
            Assert.AreEqual(new DateTime(2022, 1, 1), dateContext.BeginDate);
            Assert.AreEqual(new DateTime(2022, 12, 31).EndOfDay(), dateContext.EndDate);
            Assert.AreEqual(new DateTime(2021, 1, 1), dateContext.PreviousDate);
            Assert.AreEqual(new DateTime(2023, 1, 1), dateContext.NextDate);
        }

        [TestMethod()]
        public void YearDateContextFactory_Selected_Date_Before_MinDate_Test()
        {
            // Arrange
            var selectedDate = new DateTime(2019, 3, 8);
            var minDate = new DateTime(2020, 5, 20);
            var maxDate = new DateTime(2024, 10, 9);
            var factory = new YearDateContextFactory(selectedDate.AddHours(5), minDate, maxDate);

            // Act
            var dateContext = factory.Create();

            // Assert
            Assert.AreEqual(new DateTime(2020, 1, 1), dateContext.BeginDate);
            Assert.AreEqual(new DateTime(2020, 12, 31).EndOfDay(), dateContext.EndDate);
            Assert.IsNull(dateContext.PreviousDate);
            Assert.AreEqual(new DateTime(2021, 1, 1), dateContext.NextDate);
        }

        [TestMethod()]
        public void YearDateContextFactory_Selected_Date_After_MaxDate_Test()
        {
            // Arrange
            var selectedDate = new DateTime(2028, 12, 8);
            var minDate = new DateTime(2020, 5, 20);
            var maxDate = new DateTime(2024, 10, 9);
            var factory = new YearDateContextFactory(selectedDate.AddHours(5), minDate, maxDate);

            // Act
            var dateContext = factory.Create();

            // Assert
            Assert.AreEqual(new DateTime(2024, 1, 1), dateContext.BeginDate);
            Assert.AreEqual(new DateTime(2024, 12, 31).EndOfDay(), dateContext.EndDate);
            Assert.AreEqual(new DateTime(2023, 1, 1), dateContext.PreviousDate);
            Assert.IsNull(dateContext.NextDate);
        }
    }
}