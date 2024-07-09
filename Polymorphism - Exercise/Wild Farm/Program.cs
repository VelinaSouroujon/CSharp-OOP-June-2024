using System;
using Wild_Farm.Interfaces;
using Wild_Farm.Models;

namespace Wild_Farm
{
    public class Program
    {
        static void Main(string[] args)
        {
            IReader reader = new ConsoleReader();
            IWriter writer = new ConsoleWriter();

            Engine engine = new Engine(reader, writer);
            engine.Run();
        }
    }
}
