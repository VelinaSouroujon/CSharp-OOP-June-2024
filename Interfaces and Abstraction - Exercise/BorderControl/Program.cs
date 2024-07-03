using System;
using System.Collections.Generic;

namespace BorderControl
{
    public class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, IBuyer> buyers = new Dictionary<string, IBuyer>();

            int n = int.Parse(Console.ReadLine());
            int foodPurchased = 0;

            for (int i = 0; i < n; i++)
            {
                string[] tokens = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);

                string name = tokens[0];
                int age = int.Parse(tokens[1]);

                if (tokens.Length == 4)
                {
                    string citizenId = tokens[2];
                    DateTime birthdate = GetDate(tokens[3]);

                    buyers.Add(name, new Citizen(name, age, citizenId, birthdate));
                }
                else if(tokens.Length == 3)
                {
                    string group = tokens[2];

                    buyers.Add(name, new Rebel(name, age, group));
                }
            }

            string inputName = "";
            while((inputName = Console.ReadLine()).ToLower() != "end")
            {
                if(buyers.TryGetValue(inputName, out IBuyer buyer))
                {
                    foodPurchased += buyer.BuyFood();
                }
            }

            Console.WriteLine(foodPurchased);
        }
        
        static DateTime GetDate(string input)
        {
            string[] tokens = input.Split('/', StringSplitOptions.RemoveEmptyEntries);

            int day = int.Parse(tokens[0]);
            int month = int.Parse(tokens[1]);
            int year = int.Parse(tokens[2]);

            return new DateTime(year, month, day);
        }
    }
}
