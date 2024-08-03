using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prototype.Contracts
{
    public interface ICopyable<T>
        where T : ICopyable<T>
    {
        T ShallowCopy();
        T DeepCopy();
    }
}
