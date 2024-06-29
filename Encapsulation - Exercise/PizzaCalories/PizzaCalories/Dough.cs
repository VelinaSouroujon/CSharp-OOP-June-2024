using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaCalories
{
    public class Dough
    {
        private const double BaseCaloriesPerGram = 2;
        private const string InvalidDoughTypeMessage = "Invalid type of dough.";
        private const string InvalidWeightMessage = "Dough weight should be in the range [{0}..{1}].";

        private const double MinWeight = 1;
        private const double MaxWeight = 200;

        private string flourType;
        private string bakingTechnique;
        private double grams;

        public Dough(string flourType, string bakingTechnique, double grams)
        {
            FlourType = flourType;
            BakingTechnique = bakingTechnique;
            Grams = grams;
        }

        public string FlourType
        {
            get => flourType;

            private set
            {
                if(!FlourManager.IsFlourValid(value))
                {
                    throw new ArgumentException(InvalidDoughTypeMessage);
                }

                flourType = value;
            }
        }
        public string BakingTechnique
        {
            get => bakingTechnique;

            private set
            {
                if(!BakingTechniquesManager.IsBakingTechniqueValid(value))
                {
                    throw new ArgumentException(InvalidDoughTypeMessage);
                }

                bakingTechnique = value;
            }
        }

        public double Grams
        {
            get => grams;

            private set
            {
                if(value < MinWeight || value > MaxWeight)
                {
                    throw new ArgumentException(string.Format(InvalidWeightMessage, MinWeight, MaxWeight));
                }

                grams = value;
            }
        }

        public double GetCalories
            => BaseCaloriesPerGram
            * Grams
            * FlourManager.GetCalories(FlourType)
            * BakingTechniquesManager.GetCalories(BakingTechnique);
    }
}
