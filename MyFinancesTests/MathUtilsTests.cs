using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyFinances;
using System.Linq;

namespace MyFinancesTests
{
    [TestClass()]
    public class MathUtilsTests
    {
        [TestMethod]
        public void MathPrognosisTest()
        {
            var average = new List<decimal> {10m, 20m, 11m }.Average();
            var days = 10;
            var startValue = 300m;
            var predicted = 163.33m;
            var actual = Math.Round(MathUtils.CalculatePrognosis(days, average, startValue),2);
            Assert.AreEqual(predicted, actual);
        }
    }
}