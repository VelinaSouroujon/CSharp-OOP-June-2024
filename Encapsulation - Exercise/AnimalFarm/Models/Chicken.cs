using System;

namespace AnimalFarm.Models
{
    public class Chicken
    {
        private const int MinAge = 0;
        private const int MaxAge = 15;

        private const string InvalidNameMessage = "Name cannot be empty.";
        private const string InvalidAgeMessage = "Age should be between {0} and {1}.";

        private string name;
        private int age;

        internal Chicken(string name, int age)
        {
            Name = name;
            Age = age;
        }

        public string Name
        {
            get
            {
                return this.name;
            }

            private set
            {
                if(string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(InvalidNameMessage);
                }

                this.name = value;
            }
        }

        public int Age
        {
            get
            {
                return this.age;
            }

            private set
            {
                if(value < MinAge || value > MaxAge)
                {
                    throw new ArgumentException(string.Format(InvalidAgeMessage, MinAge, MaxAge));
                }

                this.age = value;
            }
        }

        public double ProductPerDay
        {
			get
			{				
				return this.CalculateProductPerDay();
			}
        }

        private double CalculateProductPerDay()
        {
            if (Age <= 3)
            {
                return 1.5;
            }
            if (Age <= 7)
            {
                return 2;
            }
            if(Age <= 11)
            {
                return 1;
            }

            return 0.75;
        }
    }
}
