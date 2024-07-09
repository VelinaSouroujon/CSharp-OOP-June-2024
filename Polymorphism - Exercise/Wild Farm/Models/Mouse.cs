using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wild_Farm.Interfaces;
using WildFarm.Interfaces;

namespace Wild_Farm.Models
{
    public class Mouse : Mammal
    {
        private const double WeightIncreaseAfterMeal = 0.10;
        public Mouse(string name, double weight, string livingRegion)
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
            return "Squeak";
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
            public void Visit(Vegetable vegetable) => CanEatFood = true;
            public void Visit(Fruit fruit) => CanEatFood = true;
            public void Visit(Meat meat) => CanEatFood = false;
            public void Visit(Seeds seeds) => CanEatFood = false;
        }
    }
}
