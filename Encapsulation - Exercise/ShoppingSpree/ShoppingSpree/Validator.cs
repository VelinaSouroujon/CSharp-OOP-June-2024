using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingSpree
{
    public class Validator
    {
        public static void ValidateString(string value, string propertyName)
        {
            if(string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException($"{propertyName} cannot be empty");
            }
        }
        public static void ValidateMoney(decimal value)
        {
            if(value < 0)
            {
                throw new ArgumentException("Money cannot be negative");
            }
        }
    }
}
