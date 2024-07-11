using System;
using System.Security.Principal;

namespace _6.MoneyTransactions
{
    public class Program
    {
        static void Main(string[] args)
        {
            string[] inputTokens = Console.ReadLine()
                .Split(',', StringSplitOptions.RemoveEmptyEntries);

            Dictionary<int, BankAccount> bankAccountsByNumber = new Dictionary<int, BankAccount>();

            foreach(string str in inputTokens)
            {
                string[] bankAccountInfo = str.Split('-', StringSplitOptions.RemoveEmptyEntries);

                int accountNumber = int.Parse(bankAccountInfo[0]);
                decimal bankAccountBalance = decimal.Parse(bankAccountInfo[1]);

                BankAccount bankAccount = new BankAccount(accountNumber, bankAccountBalance);
                bankAccountsByNumber.Add(accountNumber, bankAccount);
            }

            string input = "";
            while((input = Console.ReadLine()).ToLower() != "end")
            {
                string[] cmdArgs = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                string command = cmdArgs[0];
                int accountNumber = int.Parse (cmdArgs[1]);
                decimal amount = decimal.Parse(cmdArgs[2]);

                try
                {
                    if (!bankAccountsByNumber.TryGetValue(accountNumber, out BankAccount currentBankAccount))
                    {
                        throw new AccountDoesNotExistException();
                    }

                    switch (command.ToLower())
                    {
                        case "deposit":
                            currentBankAccount.Deposit(amount);
                            break;

                        case "withdraw":
                            currentBankAccount.Withdraw(amount);
                            break;

                        default:
                            throw new InvalidOperationException("Invalid command!");
                    }

                    Console.WriteLine($"Account {currentBankAccount.AccountNumber} has new balance: {currentBankAccount.Balance:f2}");
                }
                catch (InvalidOperationException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (AccountDoesNotExistException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (InsufficientBalanceException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    Console.WriteLine("Enter another command");
                }
            }
        }
    }
}
