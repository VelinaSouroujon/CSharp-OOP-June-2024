using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _6.MoneyTransactions
{
    public class AccountDoesNotExistException : Exception
    {
        private const string DefaultExceptionMessage = "Invalid account!";

        public AccountDoesNotExistException()
            : base(DefaultExceptionMessage)
        {
            
        }
        public AccountDoesNotExistException(string message)
            : base(message)
        {
            
        }
    }
}
