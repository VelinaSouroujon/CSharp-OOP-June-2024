using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Animals
{
    public abstract class Animal
    {
        private const string ExceptionMessage = "Invalid input!";

        private string name;
        private int age;
        private string gender;
        public Animal(string name, int age, string gender)
        {
            Name = name;
            Age = age;
            Gender = gender;
        }

        public string Name 
        {
            get
            {
                return name;
            }
            private set
            {
                if(string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessage);
                }
                name = value;
            }
        }
        public int Age 
        {
            get
            {
                return age;
            }
            private set
            {
                if(value < 0)
                {
                    throw new ArgumentException(ExceptionMessage);
                }
                age = value;
            }
        }
        public string Gender 
        {
            get
            {
                return gender;
            }
            private set
            {
                string inputGender = value.ToLower();
                if((inputGender != "male") && (inputGender != "female"))
                {
                    throw new ArgumentException(ExceptionMessage);
                }

                gender = value;
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine(GetType().Name);
            sb.AppendLine($"{Name} {Age} {Gender}");
            sb.AppendLine(ProduceSound());

            return sb.ToString().Trim();
        }
        public abstract string ProduceSound();
    }
}
