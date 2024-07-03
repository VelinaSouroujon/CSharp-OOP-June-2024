using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BorderControl
{
    public class Citizen : IIdentifiable, IBirthable, IWithName, IBuyer
    {
        private const int DefaultFoodIncrement = 10;
        public Citizen(string name, int age, string id, DateTime birthdate)
        {
            Name = name;
            Age = age;
            Id = id;
            Birthdate = birthdate;
        }

        public string Name { get; private set; }
        public int Age { get; private set; }
        public string Id { get; private set; }
        public DateTime Birthdate { get; private set; }

        public int Food { get; private set; }

        public int BuyFood()
        {
            Food += DefaultFoodIncrement;
            return DefaultFoodIncrement;
        }
    }
}
