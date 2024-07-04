using System;

namespace ExplicitInterfaces
{
    public class Program
    {
        static void Main(string[] args)
        {
            string input = "";
            while((input = Console.ReadLine()).ToLower() != "end")
            {
                string[] citizenInfo = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                string name = citizenInfo[0];
                string country = citizenInfo[1];
                int age = int.Parse(citizenInfo[2]);

                Citizen citizen = new Citizen(name, country, age);
                IResident resident = citizen;

                Console.WriteLine(citizen.GetName());
                Console.WriteLine(resident.GetName());

            }
        }
    }
}
