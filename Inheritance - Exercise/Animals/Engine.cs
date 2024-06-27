using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Animals
{
    public class Engine
    {
        private List<Animal> animals;

        public Engine()
        {
            animals = new List<Animal>();
        }
        public void Run()
        {
            string type;
            while ((type = Console.ReadLine()) != "Beast!")
            {
                try
                {
                    string[] animalArgs = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);

                    Animal animal = GetAnimal(type, animalArgs);
                    animals.Add(animal);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            PrintAnimals();
        }
        private Animal GetAnimal(string type, string[] args)
        {
            Animal animal;

            string name = args[0];
            int age = int.Parse(args[1]);

            string gender = string.Empty;

            if (args.Length > 2)
            {
                gender = args[2];
            }

            switch (type)
            {
                case "Dog":
                    animal = new Dog(name, age, gender);
                    break;

                case "Cat":
                    animal = new Cat(name, age, gender);
                    break;

                case "Frog":
                    animal = new Frog(name, age, gender);
                    break;

                case "Kitten":
                    animal = new Kitten(name, age);
                    break;

                case "Tomcat":
                    animal = new Tomcat(name, age);
                    break;

                default:
                    throw new ArgumentException();
            }

            return animal;
        }
        private void PrintAnimals()
        {
            foreach(Animal animal in animals)
            {
                Console.WriteLine(animal);
            }
        }
    }
}
