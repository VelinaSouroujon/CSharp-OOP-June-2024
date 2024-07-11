using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _6.MoneyTransactions
{
    public class BankAccount
    {
        private decimal balance;
        public BankAccount(int accountNumber, decimal balance)
        {
            AccountNumber = accountNumber;
            Balance = balance;
        }

        public int AccountNumber { get; }
        public decimal Balance
        {
            get => balance;
            private set
            {
                if(value < 0)
                {
                    throw new InsufficientBalanceException();
                }

                balance = value;
            }
        }

        public void Deposit(decimal amount)
        {
            Balance += amount;
        }
        public void Withdraw(decimal amount)
        {
            Balance -= amount;
        }
    }
}
