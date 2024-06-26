﻿using System;
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

            Team team = new Team("SoftUni");

            foreach (Person person in people)
            {
                team.AddPlayer(person);
            }

            Console.WriteLine($"First team has {team.FirstTeam.Count} players.");
            Console.WriteLine($"Reserve team has {team.ReserveTeam.Count} players.");

        }
    }
}
