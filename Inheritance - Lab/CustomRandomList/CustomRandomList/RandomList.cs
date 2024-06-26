using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomRandomList
{
    public class RandomList : List<string>
    {
        public string RandomString()
        {
            int index = new Random().Next(0, Count);
            string removedElement = this[index];
            this.RemoveAt(index);
            return removedElement;
        }
    }
}
