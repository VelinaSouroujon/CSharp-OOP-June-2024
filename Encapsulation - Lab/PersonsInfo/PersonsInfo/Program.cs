using System;
using System.Linq;

namespace PersonsInfo
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            Person[] people = new Person[n];

            for (int i = 0; i < n; i++)
            {
                string[] personInfo = Console.ReadLine().Split();

                string firstName = personInfo[0];
                string lastName = personInfo[1];
                int age = int.Parse(personInfo[2]);
                decimal salary = decimal.Parse(personInfo[3]);

                people[i] = new Person(firstName, lastName, age, salary);
            }

            decimal percentage = decimal.Parse(Console.ReadLine());

            foreach (Person person in people)
            {
                person.IncreaseSalary(percentage);
                Console.WriteLine(person);
            }
        }
    }
}
