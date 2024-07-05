using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionHierarchy
{
    public class AddRemoveCollection : IAdd,  IRemoveable
    {
        private Queue<string> items;

        public AddRemoveCollection()
        {
            items = new Queue<string>();
        }

        public int Add(string item)
        {
            items.Enqueue(item);
            return 0;
        }

        public string Remove()
        {
            return items.Dequeue();
        }
    }
}
