using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    }
}
