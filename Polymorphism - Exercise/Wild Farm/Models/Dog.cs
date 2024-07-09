using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wild_Farm.Interfaces;
using WildFarm.Interfaces;

namespace Wild_Farm.Models
{
    public class Dog : Mammal
    {
        private const double WeightIncreaseAfterMeal = 0.40;
        public Dog(string name, double weight, string livingRegion)
            : base(name, weight, livingRegion)
        {

        }

        protected override double WeightIncrease => WeightIncreaseAfterMeal;

        public override bool Eat(IFood food)
        {
            FoodVisitor foodVisitor = new FoodVisitor();
            food.Accept(foodVisitor);

            return foodVisitor.CanEatFood && base.Eat(food);
        }

        public override string ProduceSound()
        {
            return "Woof!";
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(base.ToString());
            sb.Append($", {Weight}, {LivingRegion}, {FoodEaten}]");

            return sb.ToString().TrimEnd();
        }

        private class FoodVisitor : IFoodVisitor
        {
            public bool CanEatFood { get; private set; }
            public void Visit(Vegetable vegetable) => CanEatFood = false;
            public void Visit(Fruit fruit) => CanEatFood = false;
            public void Visit(Meat meat) => CanEatFood = true;
            public void Visit(Seeds seeds) => CanEatFood = false;
        }
    }
}
