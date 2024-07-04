using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionHierarchy
{
    public abstract class Collection : IAdd
    {
        public abstract IReadOnlyCollection<string> Items { get; }

        public abstract int Add(string item);   
    }
}
