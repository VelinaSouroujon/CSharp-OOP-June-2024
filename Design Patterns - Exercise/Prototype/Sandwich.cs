using Prototype.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prototype
{
    public class Sandwich : ICopyable<Sandwich>
    {
        private readonly string bread;
        private readonly string meat;
        private readonly string cheese;
        private readonly string[] veggies;

        public Sandwich(string bread, string meat, string cheese, params string[] veggies)
        {
            this.bread = bread;
            this.meat = meat;
            this.cheese = cheese;
            this.veggies = veggies;
        }

        public Sandwich DeepCopy()
        {
            string[] veggiesCopy = new string[veggies.Length];
            Array.Copy(veggies, veggiesCopy, veggies.Length);

            return new Sandwich(bread, meat, cheese, veggiesCopy);
        }

        public Sandwich ShallowCopy()
        {
            return new Sandwich(bread, meat, cheese, veggies);
        }
        public override string ToString()
        {
            string veggiesInfo = string.Join(", ", veggies);
            return $"{bread}, {meat}, {cheese}, {veggiesInfo}";
        }
    }
}
