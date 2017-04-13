using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinances
{
    public class Controller
    {
        private readonly DataModel _dataModel;
        public Controller()
        {
            _dataModel = new DataModel();
        }

        public void InitializeDataModel(decimal ammount)
        {
            _dataModel.Money = ammount;
        }

        public decimal GetAmmount()
        {
            return _dataModel.Money;
        }

        public void AddTransaction(DateTime date, decimal ammount, string title, TransactionType type)
        {
           var transaction = new Transaction(date, ammount, title, type);
            switch (type)
            {
                case TransactionType.Income:
                    _dataModel.Money += ammount;
                    break;
                case TransactionType.Outcome:
                    _dataModel.Money -= ammount;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
            _dataModel.Transactions.Add(transaction);
        }

        public decimal GetAverageOutcomeValue()
        {
            var transactionsFromThisMonth =_dataModel.Transactions.Where(
                t => t.DateTime.Month.Equals(DateTime.Now.Month) && t.TransactionType.Equals(TransactionType.Outcome))
            .ToList();

            var moneyAmmounts = transactionsFromThisMonth.Select(t => t.Ammount).ToList(); 
            return MathUtils.Average(moneyAmmounts);
             
        }

        public decimal GetEndOfMonthPrognosis()
        {
            var averageTransaction = GetAverageOutcomeValue();
            var actualMoney = _dataModel.Money;
            var daysInCurrentMonth = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
            var daysInMonthLeft = daysInCurrentMonth - DateTime.Now.Day;

            return MathUtils.CalculatePrognosis(daysInMonthLeft, averageTransaction, actualMoney);
        }

        public void SaveDataModel(string path)
        {
            
        }

        public void LoadDataModel(string path)
        {
            
        }
       
    }
}
