using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MyFinances
{
    public class Controller
    {
        private DataModel _dataModel;
        private int _defaultMonth;
        public Controller()
        {
            _defaultMonth = DateTime.Now.Month;
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

        public void RemoveTransaction(Transaction t)
        {
            if(t == null)return;
            if (t.Id == 0)
            {
                _dataModel.Transactions.Remove(t);
            }
            var removedElems = _dataModel.Transactions.RemoveAll(x => x.Id == t.Id);
            if(removedElems < 1)return;
            switch (t.TransactionType)
            {
                case TransactionType.Income:
                    _dataModel.Money -= t.Ammount;
                    break;
                case TransactionType.Outcome:
                    _dataModel.Money += t.Ammount;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public int ModifyTransaction(Transaction t)
        {
            var transaction = _dataModel.Transactions.FirstOrDefault(x => x.Id == t.Id);
            if(transaction == null)
            {
                throw new IndexOutOfRangeException("Incorrect transaction ID");
            }
            var i = _dataModel.Transactions.IndexOf(transaction);
            _dataModel.Transactions[i].DateTime = t.DateTime;
            _dataModel.Transactions[i].Ammount = t.Ammount;
            _dataModel.Transactions[i].Title = t.Title;
            _dataModel.Transactions[i].TransactionType = t.TransactionType;
            _dataModel.Transactions[i].IsRegular = t.IsRegular;
            _dataModel.Transactions[i].ModifiedDate = DateTime.Now;
            return _dataModel.Transactions[i].Id;
        }

        public int AddTransaction(DateTime date, decimal ammount, string title, TransactionType type, bool isRegular = false)
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
            transaction.Id = GetNextId();
            transaction.IsRegular = isRegular;
            _dataModel.Transactions.Add(transaction);
            return transaction.Id;
        }

        public decimal GetAverageOutcomeValue(int month = -1, bool calculateDaily = true)
        {
            if (month == -1) month = _defaultMonth;
            var transactionsFromThisMonth =_dataModel.Transactions.Where(
                t => t.DateTime.Month.Equals(month) && t.TransactionType.Equals(TransactionType.Outcome) && !t.IsRegular)
            .ToList();

            if (transactionsFromThisMonth.Count == 0) return 0;

            if (!calculateDaily)
                return transactionsFromThisMonth.Average(t => t.Ammount);

            var sortedTransactions = new Dictionary<DateTime, List<Transaction>>();
            foreach(var transaction in transactionsFromThisMonth)
            {
                if (!sortedTransactions.Keys.Contains(transaction.DateTime))
                {
                    sortedTransactions.Add(transaction.DateTime, new List<Transaction>());
                }
                sortedTransactions[transaction.DateTime].Add(transaction);
            }

            var moneyAmmounts = new List<decimal>(); 

            foreach(var transactionList in sortedTransactions.Keys)
            {
                moneyAmmounts.Add(sortedTransactions[transactionList].Sum(x => x.Ammount));
            }

            return moneyAmmounts.Average();
             
        }

        public Transaction GetById(int id)
        {
            return _dataModel.Transactions.FirstOrDefault(x => x.Id == id);
        }

        private int GetNextId()
        {
            return  _dataModel.Transactions.Count == 0 ?  1 :  _dataModel.Transactions.Max(x => x.Id) + 1;
        }

        public List<Transaction> GetTransactionsList()
        {
            return _dataModel.Transactions;
        }

        public List<Transaction> GetTransactionsList(DateTime? timeSpan, TransactionType? type)
        {
            var result = _dataModel.Transactions;

            if (timeSpan != null)
            {
                var ts = timeSpan.Value;
                result = (List<Transaction>) result.Where(t => t.DateTime.Month.Equals(ts.Month));
            }

            if (type != null)
            {
                result = (List<Transaction>) result.Where(t => t.TransactionType.Equals(type));
            }

            result.Sort((t1, t2) =>t1.DateTime.CompareTo(t2.DateTime));
            return result;
        }

        internal void RemoveTransaction(int id)
        {
            var t = _dataModel.Transactions.FirstOrDefault(x => x.Id == id);
            RemoveTransaction(t);
        }

        public decimal GetEndOfMonthPrognosis()
        {
            var daysInCurrentMonth = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
            var daysInMonthLeft = daysInCurrentMonth - DateTime.Now.Day;

            return GetPrognosisFor(daysInMonthLeft);
        }

        public decimal GetPrognosisFor(int numberOfDays)
        {
            var averageTransaction = GetAverageOutcomeValue();
            var actualMoney = _dataModel.Money;
            return MathUtils.CalculatePrognosis(numberOfDays, averageTransaction, actualMoney);
        }

        public void SaveDataModel(string path)
        {
            Task.Factory.StartNew(() =>
            {
                var json = JsonConvert.SerializeObject(_dataModel);
                File.WriteAllText(path, json);
            });
        }

        public void LoadDataModel(string path)
        {
            var json = File.ReadAllText(path);
            _dataModel = JsonConvert.DeserializeObject<DataModel>(json);
        }
    }
}
