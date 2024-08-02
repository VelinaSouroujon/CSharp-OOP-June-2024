using System;

namespace Facade
{
    public class Program
    {
        static void Main(string[] args)
        {
            Car car = new CarBuilderFacade()
                .Info
                    .WithType("BMW")
                    .WithColor("Red")
                    .WithNumberOfDoors(4)
                .Built
                    .InCity("Munchen")
                    .AtAddress("Random address")
                .Build();

            Console.WriteLine(car);
        }
    }
}
