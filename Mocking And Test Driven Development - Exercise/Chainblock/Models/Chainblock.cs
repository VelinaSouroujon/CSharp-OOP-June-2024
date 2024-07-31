using TransactionManager.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Transactions;

namespace TransactionManager.Models
{
    public class Chainblock : IChainblock
    {
        private const string cannotAddTransactionWithSameIdMessage =
            "Transaction with id {0} already exists";

        private const string transactionWithGivenIdNotFoundMessage =
            "Transaction with id {0} was not found";

        private const string transactionWithGivenStatusNotFoundMessage =
            "There are no transactions with status {0}";

        private const string transactionWithGivenSenderNotFoundMessage =
            "Transaction with sender {0} was not found";

        private const string transactionWithGivenReceiverNotFoundMessage =
            "Transaction with receiver {0} was not found";

        private const string transactionNotFoundMessage =
            "Transaction was not found";

        private readonly IDictionary<int, ITransaction> transactionsById;

        public Chainblock()
        {
            transactionsById = new Dictionary<int, ITransaction>();
        }
        public int Count
            => transactionsById.Count;

        public void Add(ITransaction transaction)
        {
            if (transaction is null)
            {
                throw new ArgumentNullException(nameof(transaction));
            }
            if(transactionsById.ContainsKey(transaction.Id))
            {
                throw new InvalidOperationException
                    (string.Format(cannotAddTransactionWithSameIdMessage, transaction.Id));
            }
            transactionsById.Add(transaction.Id, transaction);
        }
        public bool Contains(int id)
        {
            return transactionsById.ContainsKey(id);
        }
        public void ChangeTransactionStatus(int id, TransactionStatus status)
        {
            if (!transactionsById.TryGetValue(id, out ITransaction transaction))
            {
                throw new ArgumentException(string.Format(transactionWithGivenIdNotFoundMessage, id));
            }

            transaction.Status = status;
        }
        public void RemoveTransactionById(int id)
        {
            if (!transactionsById.Remove(id))
            {
                throw new InvalidOperationException(string.Format(transactionWithGivenIdNotFoundMessage, id));
            }
        }
        public ITransaction GetById(int id)
        {
            if (!transactionsById.TryGetValue(id, out ITransaction transaction))
            {
                throw new InvalidOperationException(string.Format(transactionWithGivenIdNotFoundMessage, id));
            }

            return transaction;
        }
        public IEnumerable<ITransaction> GetByTransactionStatus(TransactionStatus status)
        {
            IEnumerable<ITransaction> transactions =
                transactionsById.
                Values
                .Where(x => x.Status == status)
                .OrderByDescending(x => x.Amount)
                .ThenBy(x => x.Id);

            if (!transactions.Any())
            {
                throw new InvalidOperationException(
                    string.Format(transactionWithGivenStatusNotFoundMessage, status));
            }

            return transactions;
        }
        public IEnumerable<string> GetAllSendersWithTransactionStatus(TransactionStatus status)
        {
            IEnumerable<string> senders = transactionsById
                .Values
                .Where(x => x.Status == status)
                .OrderByDescending(x => x.Amount)
                .Select(x => x.From);

            if (!senders.Any())
            {
                throw new InvalidOperationException(
                    string.Format(transactionWithGivenStatusNotFoundMessage, status));
            }

            return senders;
        }

        public IEnumerable<string> GetAllReceiversWithTransactionStatus(TransactionStatus status)
        {
            IEnumerable<string> receivers = transactionsById
                .Values
                .Where(x => x.Status == status)
                .OrderByDescending(x => x.Amount)
                .Select(x => x.To);

            if (!receivers.Any())
            {
                throw new InvalidOperationException(
                    string.Format(transactionWithGivenStatusNotFoundMessage, status));
            }

            return receivers;
        }

        public IEnumerable<ITransaction> GetAllOrderedByAmountDescendingThenById()
        {
            return transactionsById
                .Values
                .OrderByDescending(x => x.Amount)
                .ThenBy(x => x.Id);
        }

        public IEnumerable<ITransaction> GetBySenderOrderedByAmountDescending(string sender)
        {
            IEnumerable<ITransaction> transactions = transactionsById
                .Values
                .Where(x => x.From == sender)
                .OrderByDescending(x => x.Amount);

            if (!transactions.Any())
            {
                throw new InvalidOperationException
                    (string.Format(transactionWithGivenSenderNotFoundMessage, sender));
            }

            return transactions;
        }

        public IEnumerable<ITransaction> GetByReceiverOrderedByAmountThenById(string receiver)
        {
            IEnumerable<ITransaction> receivers = transactionsById
                .Values
                .Where(x => x.To == receiver)
                .OrderByDescending(x => x.Amount)
                .ThenBy(x => x.Id);

            if (!receivers.Any())
            {
                throw new InvalidOperationException
                    (string.Format(transactionWithGivenReceiverNotFoundMessage, receiver));
            }

            return receivers;
        }

        public IEnumerable<ITransaction> GetByTransactionStatusAndMaximumAmount(TransactionStatus status, double amount)
        {
            return transactionsById
                .Values
                .Where(x => x.Status == status && x.Amount <= amount)
                .OrderByDescending(x => x.Amount);
        }

        public IEnumerable<ITransaction> GetBySenderAndMinimumAmountDescending(string sender, double amount)
        {
            IEnumerable<ITransaction> transactions = transactionsById
                .Values
                .Where(x => x.From == sender && x.Amount > amount)
                .OrderByDescending(x => x.Amount);

            if(!transactions.Any())
            {
                throw new InvalidOperationException(transactionNotFoundMessage);
            }

            return transactions;
        }

        public IEnumerable<ITransaction> GetByReceiverAndAmountRange(string receiver, double lo, double hi)
        {
            IEnumerable<ITransaction> transactions = transactionsById
                .Values
                .Where(x => x.To == receiver
                && x.Amount >= lo && x.Amount < hi)
                .OrderByDescending(x => x.Amount)
                .ThenBy(x => x.Id);

            if(!transactions.Any())
            {
                throw new InvalidOperationException(transactionNotFoundMessage);
            }

            return transactions;
        }

        public IEnumerable<ITransaction> GetAllInAmountRange(double lo, double hi)
        {
            return transactionsById
                .Values
                .Where(x => x.Amount >= lo && x.Amount <= hi);
        }
    }
}
