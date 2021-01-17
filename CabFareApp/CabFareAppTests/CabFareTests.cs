using Microsoft.VisualStudio.TestTools.UnitTesting;
using CabFareApp;
using System;
using System.Collections.Generic;
using System.Text;

namespace CabFareApp.Tests {
    [TestClass()]
    public class CabFareTests {
        [TestMethod()]
        public void GetFareSingleDayTest() {
            CabFare cabFare = new CabFare();

            Assert.AreEqual((50 * 100) + (25 * 10 * 60), cabFare.GetFare(50, new DateTime(2021, 1, 5, 8, 0, 0), new DateTime(2021, 1, 5, 18, 0, 0)));
            Assert.AreEqual((50 * 100) + (45 * 8 * 60), cabFare.GetFare(50, new DateTime(2021, 1, 5, 0, 0, 0), new DateTime(2021, 1, 5, 8, 0, 0)));
            Assert.AreEqual((50 * 100) + (45 * 6 * 60), cabFare.GetFare(50, new DateTime(2021, 1, 5, 18, 0, 0), new DateTime(2021, 1, 6, 0, 0, 0)));
            Assert.AreEqual((int)(((50 * 100) + (25 * 10 * 60))*1.15), cabFare.GetFare(50, new DateTime(2021, 1, 2, 8, 0, 0), new DateTime(2021, 1, 2, 18, 0, 0)));
            Assert.AreEqual((int)(((50 * 100) + (45 * 8 * 60))*1.15), cabFare.GetFare(50, new DateTime(2021, 1, 2, 0, 0, 0), new DateTime(2021, 1, 2, 8, 0, 0)));
            Assert.AreEqual((int)(((50 * 100) + (45 * 6 * 60))*1.15), cabFare.GetFare(50, new DateTime(2021, 1, 2, 18, 0, 0), new DateTime(2021, 1, 3, 0, 0, 0)));
        }

        [TestMethod()]
        public void GetFareMultipleDaysTest() {
            CabFare cabFare = new CabFare();

            Assert.AreEqual((50 * 100) + (25 * 50 * 60) + (45 * 56 * 60), cabFare.GetFare(50, new DateTime(2021, 1, 4, 8, 0, 0), new DateTime(2021, 1, 8, 18, 0, 0)));
            Assert.AreEqual((int)(((50 * 100) + (25 * 80 * 60) + (45 * 98 * 60))*1.15), cabFare.GetFare(50, new DateTime(2021, 1, 2, 8, 0, 0), new DateTime(2021, 1, 9, 18, 0, 0)));
        }

        [TestMethod()]
        public void GetWorkHoursSingleDayTest() {
            CabFare cabFare = new CabFare();

            Assert.AreEqual(new TimeSpan(10,0,0), cabFare.GetWorkHours(new DateTime(2021, 1, 1, 8, 0, 0), new DateTime(2021, 1, 1, 18, 0, 0)));
            Assert.AreEqual(TimeSpan.Zero, cabFare.GetWorkHours(new DateTime(2021, 1, 1, 0, 0, 0), new DateTime(2021, 1, 1, 8, 0, 0)));
            Assert.AreEqual(TimeSpan.Zero, cabFare.GetWorkHours(new DateTime(2021, 1, 1, 18, 0, 0), new DateTime(2021, 1, 2, 0, 0, 0)));
            Assert.AreEqual(new TimeSpan(1, 0, 0), cabFare.GetWorkHours(new DateTime(2021, 1, 1, 8, 0, 0), new DateTime(2021, 1, 1, 9, 0, 0)));
            Assert.AreEqual(new TimeSpan(1, 0, 0), cabFare.GetWorkHours(new DateTime(2021, 1, 1, 7, 0, 0), new DateTime(2021, 1, 1, 9, 0, 0)));
            Assert.AreEqual(new TimeSpan(1, 0, 0), cabFare.GetWorkHours(new DateTime(2021, 1, 1, 17, 0, 0), new DateTime(2021, 1, 1, 19, 0, 0)));
            Assert.AreEqual(new TimeSpan(10, 0, 0), cabFare.GetWorkHours(new DateTime(2021, 1, 1, 7, 0, 0), new DateTime(2021, 1, 1, 19, 0, 0)));
        }

        [TestMethod()]
        public void GetWorkHoursMultipleDays() {
            CabFare cabFare = new CabFare();

            Assert.AreEqual(new TimeSpan(2, 0, 0), cabFare.GetWorkHours(new DateTime(2021, 1, 1, 17, 0, 0), new DateTime(2021, 1, 2, 9, 0, 0)));
            Assert.AreEqual(new TimeSpan(50, 0, 0), cabFare.GetWorkHours(new DateTime(2021, 1, 4, 8, 0, 0), new DateTime(2021, 1, 8, 18, 0, 0)));
        }

        [TestMethod()]
        public void IsWeekendTest() {
            CabFare cabFare = new CabFare();

            Assert.AreEqual(false, cabFare.IsWeekend(new DateTime(2021, 1, 1, 0, 0, 0)));
            Assert.AreEqual(true, cabFare.IsWeekend(new DateTime(2021, 1, 1, 22, 0, 0)));
            Assert.AreEqual(true, cabFare.IsWeekend(new DateTime(2021, 1, 2, 0, 0, 0)));
            Assert.AreEqual(true, cabFare.IsWeekend(new DateTime(2021, 1, 3, 0, 0, 0)));
            Assert.AreEqual(true, cabFare.IsWeekend(new DateTime(2021, 1, 4, 0, 0, 0)));
            Assert.AreEqual(false, cabFare.IsWeekend(new DateTime(2021, 1, 4, 7, 0, 0)));
        }
    }
}