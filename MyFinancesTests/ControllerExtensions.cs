using MyFinances;

namespace MyFinancesTests
{
    internal static class ControllerExtensions
    {
        public static int AddTransaction(this Controller ctr, Transaction t)
        {
            if (t == null) return 0;
            return ctr.AddTransaction(t.DateTime, t.Ammount, t.Title, t.TransactionType, t.IsRegular);
        }
    }
}
