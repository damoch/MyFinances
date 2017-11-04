using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

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

        [JsonConstructor]
        public Transaction(DateTime date, decimal ammount, string title, TransactionType type, int id)
        {
            DateTime = date;
            Ammount = ammount;
            Title = title;
            TransactionType = type;
            Id = id;
        }

        public override string ToString()
        {
            return DateTime.ToShortDateString() + " - " + Title;
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime DateTime { get; set; }
        public decimal Ammount { get; set; }
        public TransactionType TransactionType { get; set; }

        public override bool Equals(object obj)
        {
            var other = (Transaction) obj;
            return Equals(other);
        }

        protected bool Equals(Transaction other)
        {
            return Id == other.Id && string.Equals(Title, other.Title) && DateTime.Equals(other.DateTime) && Ammount == other.Ammount && TransactionType == other.TransactionType;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Id;
                hashCode = (hashCode * 397) ^ (Title != null ? Title.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ DateTime.GetHashCode();
                hashCode = (hashCode * 397) ^ Ammount.GetHashCode();
                hashCode = (hashCode * 397) ^ (int) TransactionType;
                return hashCode;
            }
        }
    }
}
