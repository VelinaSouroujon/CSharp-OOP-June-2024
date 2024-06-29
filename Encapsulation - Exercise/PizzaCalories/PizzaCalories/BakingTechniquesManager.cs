using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaCalories
{
    public static class BakingTechniquesManager
    {
        private static Dictionary<string, double> bakingTechniqueCalories = new Dictionary<string, double>()
            {
                { "crispy", 0.9 },
                { "chewy", 1.1 },
                { "homemade", 1 }
            };

        public static bool IsBakingTechniqueValid(string bakingTechnique)
        {
            return bakingTechniqueCalories.ContainsKey(bakingTechnique.ToLower());
        }
        public static double GetCalories(string bakingTechnique)
        {
            return bakingTechniqueCalories[bakingTechnique.ToLower()];
        }
    }
}
