using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionHierarchy
{
    public class MyList : Collection, IRemoveable, ICountable
    {
        private List<string> items;
        public MyList()
        {
            items = new List<string>();
            Items = items.AsReadOnly();
        }
        public override IReadOnlyCollection<string> Items { get; }

        public int Used => Items.Count;

        public override int Add(string item)
        {
            int idxToInsert = 0;

            items.Insert(idxToInsert, item);
            return idxToInsert;
        }

        public string Remove()
        {
            int idxToRemove = 0;

            string removedElement = items[idxToRemove];
            items.RemoveAt(idxToRemove);

            return removedElement;
        }
    }
}
