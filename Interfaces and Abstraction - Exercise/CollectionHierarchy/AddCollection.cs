using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionHierarchy
{
    public class AddCollection : Collection
    {
        private List<string> items;

        public AddCollection()
        {
            items = new List<string>();
            Items = items.AsReadOnly();
        }
        public override IReadOnlyCollection<string> Items { get; }

        public override int Add(string item)
        {
            items.Add(item);
            return items.Count - 1;
        }
    }
}
