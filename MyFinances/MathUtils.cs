using System;
using System.Collections.Generic;
using System.Linq;

namespace MyFinances
{
    public class MathUtils
    {
        public static decimal CalculatePrognosis(int daysInMonthLeft, decimal averageTransaction, decimal actualMoney)
        {
            return actualMoney - averageTransaction * daysInMonthLeft;
        }
    }
}
