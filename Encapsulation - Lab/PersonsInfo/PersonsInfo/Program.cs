using System;
using System.Linq;

namespace PersonsInfo
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            List<Person> people = new List<Person>();

            for (int i = 0; i < n; i++)
            {
                try
                {
                    string[] personInfo = Console.ReadLine().Split();

                    string firstName = personInfo[0];
                    string lastName = personInfo[1];
                    int age = int.Parse(personInfo[2]);
                    decimal salary = decimal.Parse(personInfo[3]);

                    Person person = new Person(firstName, lastName, age, salary);
                    people.Add(person);
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            decimal percentage = decimal.Parse(Console.ReadLine());

            people.ForEach(x => x.IncreaseSalary(percentage));
            people.ForEach(Console.WriteLine);
        }
    }
}
