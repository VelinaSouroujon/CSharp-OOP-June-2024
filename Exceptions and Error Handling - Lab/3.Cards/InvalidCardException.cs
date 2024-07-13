using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3.Cards
{
    public class InvalidCardException : Exception
    {
        private const string DefaultExceptionMessage = "Invalid card!";

        public InvalidCardException()
            : base(DefaultExceptionMessage)
        {
            
        }
        public InvalidCardException(string message)
            : base(message) 
        {
            
        }
    }
}
