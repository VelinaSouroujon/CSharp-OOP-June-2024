using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Composite.Contracts
{
    public interface IGift
    {
        string Description { get; }
        int CalculateTotalPrice();
    }
}
