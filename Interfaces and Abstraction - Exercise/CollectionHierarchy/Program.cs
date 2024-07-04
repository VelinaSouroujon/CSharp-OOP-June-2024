using System;

namespace CollectionHierarchy
{
    public class Program
    {
        static void Main(string[] args)
        {
            AddCollection addCollection = new AddCollection();
            AddRemoveCollection addRemoveCollection = new AddRemoveCollection();
            MyList myList = new MyList();

            List<IAdd> addCollections = new List<IAdd>()
            {
                addCollection,
                addRemoveCollection,
                myList
            };

            List<IRemoveable> removeableCollections = new List<IRemoveable>()
            {
                addRemoveCollection,
                myList
            };

            string[] elementsToAdd = Console.ReadLine()
                .Split(' ', StringSplitOptions.RemoveEmptyEntries);

            foreach(IAdd collection in addCollections)
            {
                foreach(string element in elementsToAdd)
                {
                    Console.Write(collection.Add(element) + " ");
                }
                Console.WriteLine();
            }

            int removeOperationsCount = int.Parse(Console.ReadLine());

            foreach(IRemoveable collection in removeableCollections)
            {
                for(int i = 0; i < removeOperationsCount; i++)
                {
                    Console.Write(collection.Remove() + " ");
                }
                Console.WriteLine();
            }
        }
    }
}
