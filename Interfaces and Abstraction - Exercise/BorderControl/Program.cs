using System;
using System.Collections.Generic;

namespace BorderControl
{
    public class Program
    {
        static void Main(string[] args)
        {
            List<IBirthable> list = new List<IBirthable>();

            string input = "";
            while((input = Console.ReadLine()).ToLower() != "end")
            {
                string[] tokens = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                string type = tokens[0].ToLower();
                if (type == "citizen")
                {
                    string citizenName = tokens[1];
                    int citizenAge = int.Parse(tokens[2]);
                    string citizenId = tokens[3];
                    DateTime citizenBirthday = GetDate(tokens[4]);

                    list.Add(new Citizen(citizenName, citizenAge, citizenId, citizenBirthday));
                }
                else if(type == "pet")
                {
                    string petName = tokens[1];
                    DateTime petBirthday = GetDate(tokens[2]);

                    list.Add(new Pet(petName, petBirthday));
                }
            }

            int yearToLookup = int.Parse(Console.ReadLine());
            PrintOutput(list, yearToLookup);        
        }
        static void PrintOutput(IEnumerable<IBirthable> list, int yearToLookup)
        {
            foreach (IBirthable birthable in list
                .Where(x => x.Birthdate.Year == yearToLookup))
            {
                Console.WriteLine($"{birthable.Birthdate.Day.ToString().PadLeft(2, '0')}" +
                    $"/{birthable.Birthdate.Month.ToString().PadLeft(2, '0')}" +
                    $"/{birthable.Birthdate.Year.ToString().PadLeft(2, '0')}");
            }
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
