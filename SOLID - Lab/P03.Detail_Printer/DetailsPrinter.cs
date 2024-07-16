using P03.Detail_Printer;
using System;
using System.Collections.Generic;

namespace P03.DetailPrinter
{
    public class DetailsPrinter
    {
        private IWriter writer;
        private IList<Employee> employees;

        public DetailsPrinter(IList<Employee> employees, IWriter writer)
        {
            this.employees = employees;
            this.writer = writer;
        }

        public void PrintDetails()
        {
            foreach (Employee employee in this.employees)
            {
                writer.WriteLine(employee.ToString());
            }
        }
    }
}
