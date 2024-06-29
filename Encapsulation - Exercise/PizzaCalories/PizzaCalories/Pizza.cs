using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaCalories
{
    public class Pizza
    {
        private const int NameMinLength = 1;
        private const int NameMaxLength = 15;
        private const string InvalidNameMessage = "Pizza name should be between {0} and {1} symbols.";

        private int MinToppingsCount = 0;
        private int MaxToppingsCount = 10;
        private const string InvalidToppingsCountMessage = "Number of toppings should be in range [{0}..{1}].";

        private string name;
        private Dough dough;
        private List<Topping> toppings;

        public Pizza(string name, Dough dough)
        {
            Name = name;
            this.dough = dough;
            toppings = new List<Topping>();
        }

        public string Name
        {
            get => name;

            private set
            {
                if(string.IsNullOrWhiteSpace(value)
                    || value.Length < NameMinLength
                    || value.Length > NameMaxLength)
                {
                    throw new ArgumentException(string.Format(InvalidNameMessage, NameMinLength, NameMaxLength));
                }

                name = value;
            }
        }
        public Dough Dough => dough;

        public int ToppingsCount => toppings.Count;

        public double Calories
            => toppings.Sum(x => x.GetCalories)
             + dough.GetCalories;

        public void AddTopping(Topping topping)
        {
            if(ToppingsCount == MaxToppingsCount)
            {
                throw new InvalidOperationException(string.Format(InvalidToppingsCountMessage, MinToppingsCount, MaxToppingsCount));
            }
            
            toppings.Add(topping);
        }

        public override string ToString()
        {
            return $"{Name} - {Calories:f2} Calories.";
        }
    }
}
