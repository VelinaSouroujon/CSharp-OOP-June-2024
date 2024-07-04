using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionHierarchy
{
    public class AddRemoveCollection : Collection, IRemoveable
    {
        private List<string> items;

        public AddRemoveCollection()
        {
            items = new List<string>();
            Items = items.AsReadOnly();
        }
        public override IReadOnlyCollection<string> Items { get; }

        public override int Add(string item)
        {
            int idxToInsert = 0;

            items.Insert(idxToInsert, item);
            return idxToInsert;
        }

        public string Remove()
        {
            int idxToRemove = items.Count - 1;

            string removedElement = items[idxToRemove];
            items.RemoveAt(idxToRemove);

            return removedElement;
        }
    }
}
