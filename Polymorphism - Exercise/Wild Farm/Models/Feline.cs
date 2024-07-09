using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wild_Farm.Models
{
    public abstract class Feline : Mammal
    {
        protected Feline(string name, double weight, string livingRegion, string breed)
            : base(name, weight, livingRegion)
        {
            Breed = breed;
        }
        public string Breed { get; private set; }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(base.ToString());
            sb.Append($", {Breed}, {Weight}, {LivingRegion}, {FoodEaten}]");

            return sb.ToString().TrimEnd();
        }
    }
}
