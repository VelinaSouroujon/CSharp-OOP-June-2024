using TransactionManager.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransactionManager.Models
{
    public class Transaction : ITransaction
    {
        public int Id { get; private set; }
        public TransactionStatus Status { get; set; }
        public string From { get; private set; }
        public string To { get; private set; }
        public double Amount { get; private set; }
    }
}
