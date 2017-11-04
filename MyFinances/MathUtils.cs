using System;
using System.Collections.Generic;
using System.Linq;

namespace MyFinances
{
    public class MathUtils
    {
        public static decimal Average(List<decimal> values)
        {
            if (values == null || values.Count == 0)
            {
                return 0;
            }
            var sum = values.Sum();
            return Math.Round((sum / values.Count),2);
        }

        public static decimal Variance(List<decimal> values)
        {
            if (values == null || values.Count == 0)
            {
                return 0;
            }
            var avg = Average(values);
            var result = (from val in values select (double) val into valTmp let avgTmp = (double) avg select (decimal) Math.Pow((valTmp - avgTmp), 2)).Sum();
            return result / values.Count;
        }

        public static decimal StandardDevaiation(List<decimal> values)
        {
            if (values == null || values.Count == 0)
            {
                return 0;
            }
            var result = (double)Variance(values);

            return (decimal)Math.Sqrt(result);
        }


        public static decimal CalculatePrognosis(int daysInMonthLeft, decimal averageTransaction, decimal actualMoney)
        {
            return actualMoney - averageTransaction * daysInMonthLeft;
        }
    }
}
