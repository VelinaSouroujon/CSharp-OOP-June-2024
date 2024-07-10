using System;
using System.Diagnostics.Metrics;

namespace _2.EnterNumbers
{
    public class Program
    {
        static void Main(string[] args)
        {
            int countOfNumbers = 10;
            int[] numbers = new int[countOfNumbers];
            int counter = 0;
            int minNum = 1;

            while(counter < countOfNumbers)
            {
                try
                {
                    int numToAdd = ReadNumber(minNum, 100);
                    minNum = numToAdd;

                    numbers[counter] = numToAdd;
                    counter++;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid Number!");
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    Console.WriteLine(ex.ParamName);
                }
            }

            Console.WriteLine(string.Join(", ", numbers));
        }
        static int ReadNumber(int start, int end)
        {
            int inputNum = int.Parse(Console.ReadLine());

            if (inputNum <= start || inputNum >= end)
            {
                throw new ArgumentOutOfRangeException($"Your number is not in range {start} - {end}!");
            }

            return inputNum;
        }
    }
}
