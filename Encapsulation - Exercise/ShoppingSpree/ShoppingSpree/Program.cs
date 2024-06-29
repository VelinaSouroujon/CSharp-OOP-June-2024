using System;
using System.Collections.Generic;

namespace ShoppingSpree
{
    public class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string[] inputPeople = Console.ReadLine().Split(';', StringSplitOptions.RemoveEmptyEntries);
                Dictionary<string, Person> people = new Dictionary<string, Person>();

                for (int i = 0; i < inputPeople.Length; i++)
                {
                    string[] personInfo = inputPeople[i].Split('=', StringSplitOptions.RemoveEmptyEntries);

                    string name = personInfo[0];
                    decimal money = decimal.Parse(personInfo[1]);

                    if (!people.ContainsKey(name))
                    {
                        people.Add(name, new Person(name, money));
                    }
                }


                string[] inputProducts = Console.ReadLine().Split(';', StringSplitOptions.RemoveEmptyEntries);
                Dictionary<string, Product> products = new Dictionary<string, Product>();

                for (int i = 0; i < inputProducts.Length; i++)
                {
                    string[] productInfo = inputProducts[i].Split('=', StringSplitOptions.RemoveEmptyEntries);

                    string name = productInfo[0];
                    decimal cost = decimal.Parse(productInfo[1]);

                    if (!products.ContainsKey(name))
                    {
                        products.Add(name, new Product(name, cost));
                    }
                }

                string input = "";
                while ((input = Console.ReadLine()) != "END")
                {
                    string[] tokens = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                    Person person = people[tokens[0]];
                    Product product = products[tokens[1]];

                    if (person.BuyProduct(product))
                    {
                        Console.WriteLine($"{person.Name} bought {product.Name}");
                    }
                    else
                    {
                        Console.WriteLine($"{person.Name} can't afford {product.Name}");
                    }
                }

                foreach (var kvp in people)
                {
                    Console.Write($"{kvp.Key} - ");

                    if (kvp.Value.Products.Count > 0)
                    {
                        Console.WriteLine(string.Join(", ", kvp.Value.Products.Select(x => x.Name)));
                    }
                    else
                    {
                        Console.WriteLine("Nothing bought");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
