using Composite.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Composite
{
    public class Gift : IGift
    {
        private readonly int price;

        public Gift(string description, int price)
        {
            Description = description;
            this.price = price;
        }

        public string Description { get; private set; }

        public int CalculateTotalPrice()
        {
            return price;
        }
        public override string ToString()
        {
            return $"{Description} -> {price}";
        }
    }
}
