using Microsoft.VisualStudio.TestTools.UnitTesting;
using WeatherApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Extensions;

namespace WeatherApp.ViewModels.Tests
{
    [TestClass()]
    public class DayDateContextFactoryTests
    {
        [TestMethod()]
        public void DayDateContextFactory_Selected_Date_In_Range_Test()
        {
            // Arrange
            var selectedDate = new DateTime(2022, 7, 1);
            var minDate = new DateTime(2022, 6, 1);
            var maxDate = new DateTime(2022, 8, 1);
            var factory = new DayDateContextFactory(selectedDate.AddHours(5), minDate, maxDate);

            // Act
            var dateContext = factory.Create();

            // Assert
            Assert.AreEqual(selectedDate, dateContext.BeginDate);
            Assert.AreEqual(new DateTime(2022, 6, 30), dateContext.PreviousDate);
            Assert.AreEqual(new DateTime(2022, 7, 2), dateContext.NextDate);
        }

        [TestMethod()]
        public void DayDateContextFactory_Selected_Date_Before_MinDate_Test()
        {
            // Arrange
            var selectedDate = new DateTime(2022, 3, 1);
            var minDate = new DateTime(2022, 6, 1);
            var maxDate = new DateTime(2022, 8, 1);
            var factory = new DayDateContextFactory(selectedDate.AddHours(5), minDate, maxDate);

            // Act
            var dateContext = factory.Create();

            // Assert
            Assert.AreEqual(minDate, dateContext.BeginDate);
            Assert.IsNull(dateContext.PreviousDate);
            Assert.AreEqual(new DateTime(2022, 6, 2), dateContext.NextDate);
        }

        [TestMethod()]
        public void DayDateContextFactory_Selected_Date_After_MaxDate_Test()
        {
            // Arrange
            var selectedDate = new DateTime(2022, 10, 1);
            var minDate = new DateTime(2022, 6, 1);
            var maxDate = new DateTime(2022, 8, 1);
            var factory = new DayDateContextFactory(selectedDate.AddHours(5), minDate, maxDate);

            // Act
            var dateContext = factory.Create();

            // Assert
            Assert.AreEqual(maxDate, dateContext.BeginDate);
            Assert.AreEqual(new DateTime(2022, 7, 31).BeginOfDay(), dateContext.PreviousDate);
            Assert.IsNull(dateContext.NextDate);
        }
    }
}