using System;
using System.Linq;

namespace _5.PlayCatch
{
    public class Program
    {
        static void Main(string[] args)
        {
            string[] elements = Console.ReadLine()
                .Split(' ', StringSplitOptions.RemoveEmptyEntries);

            int exceptionsCount = 0;

            int inputCount = 0;
            int correctInputCount = 0;
            while(exceptionsCount < 3)
            {
                string[] cmdArgs = Console.ReadLine()
                    .Split(' ', StringSplitOptions.RemoveEmptyEntries);

                inputCount++;

                string command = cmdArgs[0];

                try
                {
                    int index = GetIndex(cmdArgs[1]);
                    ValidateIndex(elements, index);

                    switch (command.ToLower())
                    {
                        case "replace":
                            string element = cmdArgs[2];
                            elements[index] = element;
                            break;

                        case "print":
                            int endIndex = GetIndex(cmdArgs[2]);
                            ValidateIndex(elements, endIndex);

                            Console.WriteLine(string.Join(", ", elements.Skip(index).Take(endIndex - index + 1)));
                            break;

                        case "show":
                            Console.WriteLine(elements[index]);
                            break;
                    }

                    correctInputCount++;
                }
                catch (ArgumentException ae)
                {
                    Console.WriteLine(ae.Message);
                }
                catch (FormatException fe)
                {
                    Console.WriteLine(fe.Message);
                }

                exceptionsCount = inputCount - correctInputCount;
            }

            Console.WriteLine(string.Join(", ", elements));
        }
        static void ValidateIndex(IList<string> collection, int index)
        {
            if(!(index >= 0 && index < collection.Count))
            {
                throw new ArgumentException("The index does not exist!");
            }
        }
        static int GetIndex(string input)
        {
            if(!int.TryParse(input, out int value))
            {
                throw new FormatException("The variable is not in the correct format!");
            }

            return value;
        }
    }
}
