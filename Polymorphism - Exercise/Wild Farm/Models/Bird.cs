using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wild_Farm.Models
{
    public abstract class Bird : Animal
    {
        protected Bird(string name, double weight, double wingSize)
            : base(name, weight)
        {
            WingSize = wingSize;
        }
        public double WingSize { get; private set; }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(base.ToString());
            sb.Append($", {WingSize}, {Weight}, {FoodEaten}]");

            return sb.ToString().TrimEnd();
        }
    }
}
