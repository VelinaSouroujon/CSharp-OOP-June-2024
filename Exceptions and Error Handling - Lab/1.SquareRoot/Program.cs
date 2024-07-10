using System;

namespace _1.SquareRoot
{
    public class Program
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());

            try
            {
                if(n < 0)
                {
                    throw new ArithmeticException("Invalid number.");
                }

                double result = Math.Sqrt(n);
                Console.WriteLine(result);
            }
            catch(ArithmeticException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.WriteLine("Goodbye.");
            }
        }
    }
}
