using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shapes
{
    public abstract class Shape
    {
        public virtual string Draw()
        {
            return $"Drawing {GetType().Name}";
        }
        public abstract double CalculatePerimeter();
        public abstract double CalculateArea();
    }
}
