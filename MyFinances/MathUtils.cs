using System;
using System.Collections.Generic;

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
            var sum = Sum(values);
            return Math.Round((sum / values.Count),2);
        }

        private static decimal Sum(List<decimal> values)
        {
            decimal sum = 0;

            foreach (var elem in values)
            {
                sum += elem;
            }

            return sum;
        }

        public static decimal Variance(List<decimal> values)
        {
            if (values == null || values.Count == 0)
            {
                return 0;
            }
            decimal avg = Average(values);
            decimal result = 0;
            foreach (var val in values)
            {
                var valTmp = (double) val;
                var avgTmp = (double) avg;
                result += (decimal) Math.Pow((valTmp - avgTmp), 2);
            }
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
            var prognosis = actualMoney;
            prognosis -= averageTransaction * daysInMonthLeft;
            //for (int i = 0; i < daysInMonthLeft; i++)
            //{
            //    prognosis += averageTransaction;
            //}
            return prognosis;
        }
    }
}
