using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionHierarchy
{
    public class MyList : IAdd, IRemoveable, ICountable
    {
        private Stack<string> items;
        public MyList()
        {
            items = new Stack<string>();
        }

        public int Used => items.Count;

        public int Add(string item)
        {
            items.Push(item);
            return 0;
        }

        public string Remove()
        {
            return items.Pop();
        }
    }
}
