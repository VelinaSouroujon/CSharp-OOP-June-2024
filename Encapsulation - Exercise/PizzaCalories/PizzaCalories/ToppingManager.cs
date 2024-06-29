using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaCalories
{
    public static class ToppingManager
    {
        private static Dictionary<string, double> toppingCalories = new Dictionary<string, double>()
        {
            { "meat", 1.2 },
            { "veggies", 0.8 },
            { "cheese", 1.1 },
            { "sauce", 0.9 }
        };

        public static bool IsToppingValid(string topping)
        {
            return toppingCalories.ContainsKey(topping.ToLower());
        }
        public static double GetCalories(string topping)
        {
            return toppingCalories[topping.ToLower()];
        }
    }
}
