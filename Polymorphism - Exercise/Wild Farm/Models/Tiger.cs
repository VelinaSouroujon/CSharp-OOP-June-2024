﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wild_Farm.Interfaces;
using WildFarm.Interfaces;

namespace Wild_Farm.Models
{
    public class Tiger : Feline
    {
        private const double WeightIncreaseAfterMeal = 1;
        public Tiger(string name, double weight, string livingRegion, string breed)
            : base(name, weight, livingRegion, breed)
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
            return "ROAR!!!";
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
