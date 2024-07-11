using System;
using System.Linq;

namespace _4._SumOfIntegers
{
    public class Program
    {
        static void Main(string[] args)
        {
            double sum = 0;

            string[] input = Console.ReadLine()
                .Split(' ');

            foreach(string str in input)
            {
                try
                {
                    int currentNumber = int.Parse(str);
                    sum += currentNumber;
                }
                catch(FormatException)
                {
                    Console.WriteLine($"The element '{str}' is in wrong format!");
                }
                catch(OverflowException)
                {
                    Console.WriteLine($"The element '{str}' is out of range!");
                }
                finally
                {
                    Console.WriteLine($"Element '{str}' processed - current sum: {sum}");
                }
            }

            Console.WriteLine($"The total sum of all integers is: {sum}");
        }
    }
}
