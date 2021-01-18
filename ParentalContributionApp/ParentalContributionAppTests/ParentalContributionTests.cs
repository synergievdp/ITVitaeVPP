using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParentalContributionApp;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParentalContributionApp.Tests {
    [TestClass()]
    public class ParentalContributionTests {
        [TestMethod()]
        public void GetContributionTest() {
            ParentalContribution parentalContribution = new ParentalContribution();

            Assert.AreEqual(0, parentalContribution.GetContribution(null, new DateTime(2021, 1, 1), false));
            Assert.AreEqual(0, parentalContribution.GetContribution(null, new DateTime(2021, 1, 1), true));

            DateTime[] offAge = { new DateTime(2010, 1, 1), new DateTime(2009, 1, 1)};
            Assert.AreEqual(5000 + 2 * 3700, parentalContribution.GetContribution(offAge, new DateTime(2021, 1, 1), false));
            Assert.AreEqual((5000 + 2 * 3700)*0.75, parentalContribution.GetContribution(offAge, new DateTime(2021, 1, 1), true));

            DateTime[] belowAge = { new DateTime(2011, 1, 1), new DateTime(2013, 1, 1) };
            Assert.AreEqual(5000 + 2 * 2500, parentalContribution.GetContribution(belowAge, new DateTime(2021, 1, 1), false));
            Assert.AreEqual((5000 + 2 * 2500) * 0.75, parentalContribution.GetContribution(belowAge, new DateTime(2021, 1, 1), true));

            DateTime[] mixedAge = { new DateTime(2010, 1, 1), new DateTime(2013, 1, 1) };
            Assert.AreEqual(5000 + 3700 + 2500, parentalContribution.GetContribution(mixedAge, new DateTime(2021, 1, 1), false));
            Assert.AreEqual((5000 + 3700 + 2500) * 0.75, parentalContribution.GetContribution(mixedAge, new DateTime(2021, 1, 1), true));

            DateTime[] maxContrib = { new DateTime(2010, 1, 1), new DateTime(2009, 1, 1), new DateTime(2011, 1, 1), new DateTime(2013, 1, 1) };
            Assert.AreEqual(15000, parentalContribution.GetContribution(maxContrib, new DateTime(2021, 1, 1), false));
            Assert.AreEqual(15000 * 0.75, parentalContribution.GetContribution(maxContrib, new DateTime(2021, 1, 1), true));

        }

        [TestMethod()]
        public void GetContributionAmountTest() {
            ParentalContribution parentalContribution = new ParentalContribution();

            Assert.AreEqual(2500, parentalContribution.GetContributionAmount(2500, 1, 3));
            Assert.AreEqual(7500, parentalContribution.GetContributionAmount(2500, 5, 3));
        }

        [TestMethod()]
        public void PassedAgeTest() {
            ParentalContribution parentalContribution = new ParentalContribution();

            Assert.AreEqual(true, parentalContribution.PassedAge(new DateTime(1995, 7, 19), new DateTime(2021, 1, 1), 25));
            Assert.AreEqual(false, parentalContribution.PassedAge(new DateTime(1995, 7, 19), new DateTime(2021, 1, 1), 26));
            Assert.AreEqual(false, parentalContribution.PassedAge(DateTime.Now, DateTime.Now, 1));
        }
    }
}