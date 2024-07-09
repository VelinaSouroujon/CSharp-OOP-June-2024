using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WildFarm.Interfaces;

namespace Wild_Farm.Interfaces
{
    public interface IFood
    {
        int Quantity { get; }
        void Accept(IFoodVisitor visitor);
    }
}
