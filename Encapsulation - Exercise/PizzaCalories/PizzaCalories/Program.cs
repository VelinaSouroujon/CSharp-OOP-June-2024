using System;

namespace PizzaCalories
{
    public class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string[] inputPizzaName = Console.ReadLine().Split();
                string pizzaName = inputPizzaName[1];

                string[] doughInfo = Console.ReadLine().Split();

                string doughFlourType = doughInfo[1];
                string doughBakingTechnique = doughInfo[2];
                double doughGrams = double.Parse(doughInfo[3]);

                Dough dough = new Dough(doughFlourType, doughBakingTechnique, doughGrams);

                Pizza pizza = new Pizza(pizzaName, dough);

                string inputTopping = "";
                while((inputTopping = Console.ReadLine()).ToLower() != "end")
                {
                    string[] toppingInfo = inputTopping.Split();

                    string toppingName = toppingInfo[1];
                    double toppingGrams = double.Parse(toppingInfo[2]);

                    pizza.AddTopping(new Topping(toppingName, toppingGrams));
                }

                Console.WriteLine(pizza);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
