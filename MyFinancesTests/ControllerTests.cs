using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyFinances;

namespace MyFinancesTests
{
    [TestClass()]
    public class ControllerTests
    {
        private Controller controller;
        public ControllerTests()
        {
            controller = new Controller();
        }
        [TestMethod()]
        public void AddTransactionTest()
        {
            controller.AddTransaction(DateTime.Now, 50.0m, "test", TransactionType.Income, false);
            Assert.AreEqual(50.0m, controller.GetAmmount());

        }

        [TestMethod()]
        public void AddMinusTransactionTest()
        {
            controller.AddTransaction(DateTime.Now, 50.0m, "test1", TransactionType.Income, false);
            controller.AddTransaction(DateTime.Now, 25.50m, "test2", TransactionType.Outcome, false);
            Assert.AreEqual(24.50m,controller.GetAmmount());
        }

        [TestMethod()]
        public void PrognosisTest()
        {
            var starter = 300m;
            controller.InitializeDataModel(starter);
            controller.AddTransaction(DateTime.Today, 24m, "test1", TransactionType.Outcome, false);
            controller.AddTransaction(DateTime.Today, 30m, "test2", TransactionType.Outcome, false);
            controller.AddTransaction(DateTime.Today, 14m, "test3", TransactionType.Outcome, false);
            Assert.Inconclusive("End of month prognosis test resut: " + controller.GetEndOfMonthPrognosis());
        }

        [TestMethod]
        public void RemoveTransactionTest()
        {
            var t = new Transaction( DateTime.Today, 10m, "tst", TransactionType.Outcome, 1);
            var count = controller.GetTransactionsList().Count;
            controller.AddTransaction(t);
            controller.RemoveTransaction(t);
            var newCount = controller.GetTransactionsList().Count;
            Assert.AreEqual(count, newCount);
        }
        [TestMethod]
        public void RemoveTransactionWithoutIdTest()
        {
            var t = new Transaction(DateTime.Today, 1m, "tst", TransactionType.Income);
            var count = controller.GetTransactionsList().Count;
            t.Id = controller.AddTransaction(t);
            controller.RemoveTransaction(t);
            var newCount = controller.GetTransactionsList().Count;
            Assert.AreEqual(count, newCount);
        }

        [TestMethod]
        public void RemoveNullTransactionTest()
        {
            controller.RemoveTransaction(null);
        }

        [TestMethod]
        public void AddNullTransactionTest()
        {
            controller.AddTransaction(null);
        }

        [TestMethod]
        public void AddInvalidTransactionTest()
        {
            var t = new Transaction(DateTime.Now, 0, null, TransactionType.Income);
            controller.AddTransaction(t);
            var x = t.ToString();
        }

        [TestMethod]
        public void PrognosisWithSetDaysTest()
        {
            var starter = 300m;
            controller.InitializeDataModel(starter);
            controller.AddTransaction(DateTime.Today, 24m, "test1", TransactionType.Outcome, false);
            controller.AddTransaction(DateTime.Today, 30m, "test2", TransactionType.Outcome, false);
            controller.AddTransaction(DateTime.Today, 14m, "test3", TransactionType.Outcome, false);
            var prognosis = controller.GetPrognosisFor(10);
            var predicted = 5.33m;
            Assert.AreEqual(predicted, prognosis);
        }

        [TestMethod]
        public void PrognosisWithSetDaysAndRegularTransactionTest()
        {
            var starter = 300m;
            controller.InitializeDataModel(starter);
            controller.AddTransaction(DateTime.Today, 24m, "test1", TransactionType.Outcome, false);
            controller.AddTransaction(DateTime.Today, 30m, "test2", TransactionType.Outcome, false);
            controller.AddTransaction(DateTime.Today, 14m, "test3", TransactionType.Outcome, false);
            controller.AddTransaction(DateTime.Today, 2m, "test3", TransactionType.Outcome, true);
            var prognosis = controller.GetPrognosisFor(10);
            var predicted = 3.33m;
            Assert.AreEqual(predicted, prognosis);
        }
    }
}