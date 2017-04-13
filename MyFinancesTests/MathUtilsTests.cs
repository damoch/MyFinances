using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyFinances;

namespace MyFinancesTests
{
    [TestClass()]
    public class MathUtilsTests
    {
        [TestMethod()]
        public void AverageTest()
        {
            var testData = new List<decimal> {1,2,3,4,5,67};
            var result = MathUtils.Average(testData);
            Assert.AreEqual(13.67m,result);
        }
    }
}