using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _6.MoneyTransactions
{
    public class InsufficientBalanceException : Exception
    {
        private const string DefaultExceptionMessage = "Insufficient balance!";
        public InsufficientBalanceException()
            : base(DefaultExceptionMessage)
        {

        }
        public InsufficientBalanceException(string message)
            : base(message)
        {
            
        }
    }
}
