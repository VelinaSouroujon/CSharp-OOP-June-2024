using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AuthorProblem
{
    public class Tracker
    {
        public void PrintMethodsByAuthor()
        {
            Type type = typeof(StartUp);

            MethodInfo[] methods = type.GetMethods(
                BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);

            foreach (MethodInfo method in methods)
            {
                AuthorAttribute author = method.GetCustomAttribute<AuthorAttribute>();
                if (author is not null)
                {
                    Console.WriteLine($"{method.Name} is written by {author.Name}");
                }
            }
        }
    }
}
