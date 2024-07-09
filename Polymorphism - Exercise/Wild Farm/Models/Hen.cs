using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wild_Farm.Interfaces;
using WildFarm.Interfaces;

namespace Wild_Farm.Models
{
    public class Hen : Bird
    {
        private const double WeightIncreaseAfterMeal = 0.35;
        public Hen(string name, double weight, double wingSize)
            : base(name, weight, wingSize)
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
            return "Cluck";
        }
        private class FoodVisitor : IFoodVisitor
        {
            public bool CanEatFood { get; private set; }
            public void Visit(Vegetable vegetable) => CanEatFood = true;
            public void Visit(Fruit fruit) => CanEatFood = true;
            public void Visit(Meat meat) => CanEatFood = true;
            public void Visit(Seeds seeds) => CanEatFood = true;
        }
    }
}
