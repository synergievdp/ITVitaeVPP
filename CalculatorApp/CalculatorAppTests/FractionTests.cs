using Microsoft.VisualStudio.TestTools.UnitTesting;
using CalculatorApp;
using System;
using System.Collections.Generic;
using System.Text;

namespace CalculatorApp.Tests {
    [TestClass()]
    public class FractionTests {

        [TestMethod()]
        public void CreateFromString() {
            Assert.AreEqual(Fraction.Create(7, 5), Fraction.Create("1,4"));
            Assert.AreEqual(Fraction.Create(165, 1), Fraction.Create("165"));
        }

        [TestMethod()]
        public void SimplifyTest() {
            Assert.AreEqual(Fraction.Create(2, 9), Fraction.Create(24, 108));
        }

        [TestMethod()]
        public void PrimesTest() {
            Dictionary<int, int> expected = new Dictionary<int, int>();
            expected.Add(2, 2);
            expected.Add(3, 1);
            // expected.Select(i => $"{i.Key}: {i.Value}").ToList().ForEach(Console.WriteLine);

            Dictionary<int, int> actual = Fraction.Primes(12);
            // actual.Select(i => $"{i.Key}: {i.Value}").ToList().ForEach(Console.WriteLine);

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void GCFTest() {
            Assert.AreEqual(12, Fraction.GCF(24, 108));
        }

        [TestMethod()]
        public void RecurringDecimals() {
            Assert.AreEqual(1, Fraction.Create(1, 3).Multiply(Fraction.Create(3, 1)).ToDecimal());
        }

        [TestMethod()]
        public void BinaryRounding() {
            Assert.AreEqual(231, Fraction.Create("1,4").Multiply(Fraction.Create("165")).ToDecimal());
        }
    }
}