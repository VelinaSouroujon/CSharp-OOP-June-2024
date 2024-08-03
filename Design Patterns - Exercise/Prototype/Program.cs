using System;

namespace Prototype
{
    public class Program
    {
        static void Main(string[] args)
        {
            string[] veggies = new string[] { "Garlic", "Tomato" };
            Sandwich sandwich = new Sandwich("White", "Turkey", "Swiss", veggies);

            Sandwich shallowCopy = sandwich.ShallowCopy();
            Sandwich deepCopy = sandwich.DeepCopy();

            veggies[0] = "Carrot";

            Console.WriteLine($"Original: {sandwich}");
            Console.WriteLine($"Shallow copy: {shallowCopy}");
            Console.WriteLine($"Deep copy: {deepCopy}");
        }
    }
}
