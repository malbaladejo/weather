using WeatherApp.Extensions;

namespace WeatherApp.ViewModels.Tests
{
    [TestClass()]
    public class WeekDateContextFactoryTests
    {
        [TestMethod()]
        public void WeekDateContextFactory_Selected_Date_In_Range_Test()
        {
            // Arrange
            var selectedDate = new DateTime(2022, 7, 1);
            var minDate = new DateTime(2022, 6, 1);
            var maxDate = new DateTime(2022, 8, 1);
            var factory = new WeekDateContextFactory(selectedDate.AddHours(5), minDate, maxDate);

            // Act
            var dateContext = factory.Create();

            // Assert
            Assert.AreEqual(new DateTime(2022, 6, 27), dateContext.BeginDate);
            Assert.AreEqual(new DateTime(2022, 7,3).EndOfDay(), dateContext.EndDate);
            Assert.AreEqual(new DateTime(2022, 6,20), dateContext.PreviousDate);
            Assert.AreEqual(new DateTime(2022, 7, 4), dateContext.NextDate);
        }

        [TestMethod()]
        public void WeekDateContextFactory_Selected_Date_Before_MinDate_Test()
        {
            // Arrange
            var selectedDate = new DateTime(2022, 3, 1);
            var minDate = new DateTime(2022, 6, 1);
            var maxDate = new DateTime(2022, 8, 1);
            var factory = new WeekDateContextFactory(selectedDate.AddHours(5), minDate, maxDate);

            // Act
            var dateContext = factory.Create();

            // Assert
            Assert.AreEqual(new DateTime(2022, 5, 30), dateContext.BeginDate);
            Assert.AreEqual(new DateTime(2022, 6, 5).EndOfDay(), dateContext.EndDate);
            Assert.IsNull(dateContext.PreviousDate);
            Assert.AreEqual(new DateTime(2022, 6, 6), dateContext.NextDate);
        }

        [TestMethod()]
        public void WeekDateContextFactory_Selected_Date_After_MaxDate_Test()
        {
            // Arrange
            var selectedDate = new DateTime(2022, 10, 1);
            var minDate = new DateTime(2022, 6, 1);
            var maxDate = new DateTime(2022, 8, 1);
            var factory = new WeekDateContextFactory(selectedDate.AddHours(5), minDate, maxDate);

            // Act
            var dateContext = factory.Create();

            // Assert
            Assert.AreEqual(new DateTime(2022, 8, 1), dateContext.BeginDate);
            Assert.AreEqual(new DateTime(2022, 8, 7).EndOfDay(), dateContext.EndDate);
            Assert.AreEqual(new DateTime(2022, 7, 25), dateContext.PreviousDate);
            Assert.IsNull(dateContext.NextDate);
        }

        [TestMethod()]
        public void WeekDateContextFactory_Selected_Date_In_LastWeek_Test()
        {
            // Arrange
            var selectedDate = new DateTime(2022,8,4);
            var minDate = new DateTime(2022, 6, 1);
            var maxDate = new DateTime(2022, 8, 5);
            var factory = new WeekDateContextFactory(selectedDate.AddHours(5), minDate, maxDate);

            // Act
            var dateContext = factory.Create();

            // Assert
            Assert.AreEqual(new DateTime(2022, 8, 1), dateContext.BeginDate);
            Assert.AreEqual(new DateTime(2022, 8, 7).EndOfDay(), dateContext.EndDate);
            Assert.AreEqual(new DateTime(2022, 7, 25), dateContext.PreviousDate);
            Assert.IsNull(dateContext.NextDate);
        }
    }
}