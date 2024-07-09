using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wild_Farm.Interfaces;

namespace Wild_Farm.Models
{
    public class Engine : IEngine
    {
        private IReader reader;
        private IWriter writer;

        private List<IAnimal> animals;

        public Engine(IReader reader, IWriter writer)
        {
            this.reader = reader;
            this.writer = writer;

            animals = new List<IAnimal>();
        }

        public void Run()
        {
            string animalArgs = "";
            while((animalArgs = reader.ReadLine()).ToLower() != "end")
            {
                string foodArgs = reader.ReadLine();

                IAnimal animal = GetAnimal(animalArgs);
                IFood food = GetFood(foodArgs);

                writer.WriteLine(animal.ProduceSound());

                if(!animal.Eat(food))
                {
                    writer.WriteLine($"{animal.GetType().Name} does not eat {food.GetType().Name}!");
                }

                animals.Add(animal);
            }

            PrintAnimalInfo();
        }
        private IAnimal GetAnimal(string args)
        {
            string[] animalInfo = args
                .Split(' ', StringSplitOptions.RemoveEmptyEntries);

            string animalType = animalInfo[0];
            string animalName = animalInfo[1];
            double animalWeight = double.Parse(animalInfo[2]);

            switch(animalType.ToLower())
            {
                case "hen":
                    double wingSize = double.Parse(animalInfo[3]);
                    return new Hen(animalName, animalWeight, wingSize);

                case "owl":
                    wingSize = double.Parse(animalInfo[3]);
                    return new Owl(animalName, animalWeight, wingSize);

                case "mouse":
                    string livingRegion = animalInfo[3];
                    return new Mouse(animalName, animalWeight, livingRegion);

                case "cat":
                    livingRegion = animalInfo[3];
                    string breed = animalInfo[4];

                    return new Cat(animalName, animalWeight, livingRegion, breed);

                case "dog":
                    livingRegion = animalInfo[3];
                    return new Dog(animalName, animalWeight, livingRegion);

                case "tiger":
                    livingRegion = animalInfo[3];
                    breed = animalInfo[4];

                    return new Tiger(animalName, animalWeight, livingRegion, breed);

                default:
                    throw new ArgumentException();
            }
        }
        private IFood GetFood(string args)
        {
            string[] foodInfo = args
                .Split(' ', StringSplitOptions.RemoveEmptyEntries);

            string foodType = foodInfo[0];
            int foodQuantity = int.Parse(foodInfo[1]);

            switch(foodType.ToLower())
            {
                case "vegetable":
                    return new Vegetable(foodQuantity);

                case "fruit":
                    return new Fruit(foodQuantity);

                case "meat":
                    return new Meat(foodQuantity);

                case "seeds":
                    return new Seeds(foodQuantity);

                default:
                    throw new ArgumentException();
            }
        }
        private void PrintAnimalInfo()
        {
            foreach(IAnimal animal in animals)
            {
                writer.WriteLine(animal.ToString());
            }
        }
    }
}
