using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wild_Farm.Interfaces;
using Wild_Farm.Models;

namespace WildFarm.Interfaces
{
    public interface IFoodVisitor
    {
        void Visit(Vegetable vegetable);
        void Visit(Fruit fruit);
        void Visit(Meat meat);
        void Visit(Seeds seeds);
    }
}
