using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wild_Farm.Interfaces;
using WildFarm.Interfaces;

namespace Wild_Farm.Models
{
    public class Vegetable : Food
    {
        public Vegetable(int quantity)
            : base(quantity)
        {

        }
        public override void Accept(IFoodVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
