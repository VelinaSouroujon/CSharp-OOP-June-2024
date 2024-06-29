using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaCalories
{
    public class Topping
    {
        private const double BaseCaloriesPerGram = 2;
        private const string InvalidToppingMessage = "Cannot place {0} on top of your pizza.";
        private const string InvalidWeightMessage = "{0} weight should be in the range [{1}..{2}].";
        private const double MinWeight = 1;
        private const double MaxWeight = 50;

        private string type;
        private double grams;

        public Topping(string type, double grams)
        {
            Type = type;
            Grams = grams;
        }

        public string Type
        {
            get => type;

            private set
            {
                if(!ToppingManager.IsToppingValid(value))
                {
                    throw new ArgumentException(string.Format(InvalidToppingMessage, value));
                }

                type = value;
            }
        }

        public double Grams
        {
            get => grams;

            private set
            {
                if(value < MinWeight || value > MaxWeight)
                {
                    throw new ArgumentException(string.Format(InvalidWeightMessage, Type, MinWeight, MaxWeight));
                }

                grams = value;
            }
        }

        public double GetCalories =>
            BaseCaloriesPerGram
            * Grams
            * ToppingManager.GetCalories(Type);
    }
}
