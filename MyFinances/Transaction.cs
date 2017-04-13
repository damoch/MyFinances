using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinances
{
    public class Transaction
    {
        public Transaction(DateTime date, decimal ammount)
        {
            DateTime = date;
            Ammount = ammount;
        }

        public Transaction(DateTime date, decimal ammount, string title)
        {
            DateTime = date;
            Ammount = ammount;
            Title = title;
        }

        public Transaction(DateTime date, decimal ammount, string title, TransactionType type)
        {
            DateTime = date;
            Ammount = ammount;
            Title = title;
            TransactionType = type;
        }

        public string Title { get; set; }
        public DateTime DateTime { get; set; }
        public decimal Ammount { get; set; }
        public TransactionType TransactionType { get; set; }
    }
}
