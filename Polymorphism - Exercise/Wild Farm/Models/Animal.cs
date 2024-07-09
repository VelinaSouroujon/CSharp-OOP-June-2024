using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wild_Farm.Interfaces;
using WildFarm.Interfaces;

namespace Wild_Farm.Models
{
    public abstract class Animal : IAnimal
    {
        protected Animal(string name, double weight)
        {
            Name = name;
            Weight = weight;
        }

        public string Name { get; private set; }

        public double Weight { get; private set; }

        public int FoodEaten { get; private set; }
        protected abstract double WeightIncrease { get; }

        public virtual bool Eat(IFood food)
        {
            FoodEaten += food.Quantity;
            Weight += WeightIncrease * food.Quantity;

            return true;
        }
        public abstract string ProduceSound();

        public override string ToString()
        {
            return $"{GetType().Name} [{Name}";
        }
    }
}
