using Composite.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Composite
{
    public class CompositeGift : IGift
    {
        private readonly List<IGift> children = new List<IGift>();

        public CompositeGift(string description)
        {
            Description = description;
        }

        public string Description { get; private set; }
        public void AddGift(IGift gift)
        {
            if(gift is null)
            {
                throw new ArgumentNullException(nameof(gift));
            }

            children.Add(gift);
        }

        public int CalculateTotalPrice()
        {
            return children.Sum(x => x.CalculateTotalPrice());
        }
        public override string ToString()
        {
            return $"{Description}({string.Join(", ", children)}) -> {CalculateTotalPrice()}";
        }
    }
}
