using System;

namespace MyFinances
{
    class ConvertUtils
    {
        public static double StringToDouble(string input)
        {
            double result; input = input.Trim();
            if (!double.TryParse(input.Replace(',', '.'), out result) && !double.TryParse(input.Replace('.', ','), out result))
                throw new Exception("Conversion to double failed");

            return result;
        }

        public static decimal StringToDecimal(string input)
        {
            decimal result; input = input.Trim();
            if (!decimal.TryParse(input.Replace(',', '.'), out result) && !decimal.TryParse(input.Replace('.', ','), out result))
                throw new Exception("Conversion to double failed");

            return result;
        }
    }
}
