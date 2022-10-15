using WeatherApp.Extensions;

namespace WeatherApp.ViewModels.Tests
{
    [TestClass()]
    public class MonthDateContextFactoryTests
    {
        [TestMethod()]
        public void MonthDateContextFactory_Selected_Date_In_Range_Test()
        {
            // Arrange
            var selectedDate = new DateTime(2022, 7, 8);
            var minDate = new DateTime(2022, 5, 20);
            var maxDate = new DateTime(2022, 10, 9);
            var factory = new MonthDateContextFactory(selectedDate.AddHours(5), minDate, maxDate);

            // Act
            var dateContext = factory.Create();

            // Assert
            Assert.AreEqual(new DateTime(2022, 7, 1), dateContext.BeginDate);
            Assert.AreEqual(new DateTime(2022, 7, 31).EndOfDay(), dateContext.EndDate);
            Assert.AreEqual(new DateTime(2022, 6, 1), dateContext.PreviousDate);
            Assert.AreEqual(new DateTime(2022, 8, 1), dateContext.NextDate);
        }

        [TestMethod()]
        public void MonthDateContextFactory_Selected_Date_Before_MinDate_Test()
        {
            // Arrange
            var selectedDate = new DateTime(2022, 3, 8);
            var minDate = new DateTime(2022, 5, 20);
            var maxDate = new DateTime(2022, 10, 9);
            var factory = new MonthDateContextFactory(selectedDate.AddHours(5), minDate, maxDate);

            // Act
            var dateContext = factory.Create();

            // Assert
            Assert.AreEqual(new DateTime(2022, 5, 1), dateContext.BeginDate);
            Assert.AreEqual(new DateTime(2022, 5, 31).EndOfDay(), dateContext.EndDate);
            Assert.IsNull(dateContext.PreviousDate);
            Assert.AreEqual(new DateTime(2022, 6, 1), dateContext.NextDate);
        }

        [TestMethod()]
        public void MonthDateContextFactory_Selected_Date_After_MaxDate_Test()
        {
            // Arrange
            var selectedDate = new DateTime(2022, 12, 8);
            var minDate = new DateTime(2022, 5, 20);
            var maxDate = new DateTime(2022, 10, 9);
            var factory = new MonthDateContextFactory(selectedDate.AddHours(5), minDate, maxDate);

            // Act
            var dateContext = factory.Create();

            // Assert
            Assert.AreEqual(new DateTime(2022, 10, 1), dateContext.BeginDate);
            Assert.AreEqual(new DateTime(2022, 10, 31).EndOfDay(), dateContext.EndDate);
            Assert.AreEqual(new DateTime(2022, 9, 1), dateContext.PreviousDate);
            Assert.IsNull(dateContext.NextDate);
        }
    }
}