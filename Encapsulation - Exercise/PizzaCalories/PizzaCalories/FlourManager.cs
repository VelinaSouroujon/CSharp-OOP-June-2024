using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaCalories
{
    public static class FlourManager
    {
        private static Dictionary<string, double> flourTypesCalories = new Dictionary<string, double>()
            {
                { "white", 1.5 },
                { "wholegrain", 1 }
            };
        public static bool IsFlourValid(string flourType)
        {
            return flourTypesCalories.ContainsKey(flourType.ToLower());
        }
        public static double GetCalories(string flourType)
        {
            return flourTypesCalories[flourType.ToLower()];
        }
    }
}
