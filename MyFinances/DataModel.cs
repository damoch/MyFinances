using System.Collections.Generic;
using Newtonsoft.Json;

namespace MyFinances
{
    public class DataModel
    {
        public decimal Money { get; set; }
        public List<Transaction> Transactions { get; set; }
        public DataModel()
        {
            Transactions = new List<Transaction>();
        }

        [JsonConstructor]
        public DataModel(List<Transaction> transactions, decimal money)
        {
            Transactions = transactions;
            Money = money;
        }

    }
}
