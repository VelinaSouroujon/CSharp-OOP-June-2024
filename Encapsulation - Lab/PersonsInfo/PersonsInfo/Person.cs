using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonsInfo
{
    public class Person
    {
        private const int MinNameLength = 3;
        private const int MinAge = 1;
        private const decimal MinSalary = 650;

        private const string InvalidNameMessage = "{0} name cannot contain fewer than {1} symbols!";
        private const string InvalidAgeMessage = "Age cannot be zero or a negative integer!";
        private const string InvalidSalaryMessage = "Salary cannot be less than {0} leva!";

        private string firstName;
        private string lastName;
        private int age;
        private decimal salary;
        public Person(string firstName, string lastName, int age, decimal salary)
        {
            FirstName = firstName;
            LastName = lastName;
            Age = age;
            Salary = salary;
        }

        public string FirstName 
        {
            get
            {
                return firstName;
            }
            private set
            {
                ValidateName("First", value);

                firstName = value;
            }
        }
        public string LastName 
        {
            get
            {
                return lastName;
            }
            private set
            {
                ValidateName("Last", value);

                lastName = value;
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
                if (value < MinAge)
                {
                    throw new ArgumentException(InvalidAgeMessage);
                }

                age = value;
            }
        }
        public decimal Salary 
        {
            get
            {
                return salary;
            }
            private set
            {
                if(value < MinSalary)
                {
                    throw new ArgumentException(string.Format(InvalidSalaryMessage, MinSalary));
                }

                salary = value;
            }
        }

        public void IncreaseSalary(decimal percentage)
        {
            if(Age < 30)
            {
                percentage *= 0.5M;
            }

            Salary = Salary + (percentage / 100) * Salary;
        }

        public override string ToString()
        {
            return $"{FirstName} {LastName} receives {Salary:f2} leva.";
        }
        private void ValidateName(string nameType, string valueOfName)
        {
            if(valueOfName.Length < MinNameLength)
            {
                throw new ArgumentException(string.Format(InvalidNameMessage, nameType, MinNameLength));
            }
        }
    }
}
