using P03.Detail_Printer;
using System.Collections.Generic;

namespace P03.DetailPrinter
{
    class Program
    {
        static void Main()
        {
            List<Employee> employees = new List<Employee>()
            {
                new Employee("Pesho"),
                new Manager("Gosho", new List<string> {"CV", "Contract"})
            };

            IWriter writer = new ConsoleWriter();

            DetailsPrinter detailsPrinter = new DetailsPrinter(employees, writer);

            detailsPrinter.PrintDetails();
        }
    }
}
