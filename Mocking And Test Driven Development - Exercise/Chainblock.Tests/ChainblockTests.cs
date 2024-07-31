using TransactionManager.Contracts;
using TransactionManager.Models;
using NUnit.Framework;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TransactionManager.Tests
{
    public class ChainblockTests
    {
        private IChainblock chainblock;

        private static readonly TransactionStatus[] allStatuses =
            Enum.GetValues<TransactionStatus>();

        [SetUp]
        public void Setup()
        {
            chainblock = new Chainblock();
        }

        [Test]
        public void Initialize_ShouldWorkCorrectly()
        {
            Assert.AreEqual(0, chainblock.Count);
        }
        [Test]
        public void Add_ValidData_ShouldAddTransaction()
        {
            ITransaction[] transactions = GetTransactions();

            for (int i = 0; i < transactions.Length; i++)
            {
                chainblock.Add(transactions[i]);

                Assert.AreEqual(i + 1, chainblock.Count);

                AssertExist(transactions, 0, i);
            }
        }
        [Test]
        public void Add_NotUnique_ThrowInvalidOperationException()
        {
            ITransaction transaction1 = GetTransaction();
            ITransaction transaction2 = GetTransaction(SetupId(transaction1.Id));

            chainblock.Add(transaction1);

            Assert.Throws<InvalidOperationException>(() => chainblock.Add(transaction2));
        }
        [Test]
        public void AddNull_ThrowArgumentException()
        {
            Assert.Throws<ArgumentNullException>(() => chainblock.Add(null));
        }
        [Test]
        public void Contains_ValidData_ShouldWorkCorrectly()
        {
            ITransaction transaction = GetTransaction();

            Assert.IsFalse(chainblock.Contains(transaction.Id - 1));
            Assert.IsFalse(chainblock.Contains(transaction.Id));
            Assert.IsFalse(chainblock.Contains(transaction.Id + 1));

            chainblock.Add(transaction);

            Assert.IsFalse(chainblock.Contains(transaction.Id - 1));
            Assert.IsTrue(chainblock.Contains(transaction.Id));
            Assert.IsFalse(chainblock.Contains(transaction.Id + 1));
        }
        [Test]
        public void GetById_NotFound_ThrowInvalidOperationException()
        {
            ITransaction originalTransaction = GetTransaction();

            Assert.Throws<InvalidOperationException>(() => chainblock.GetById(originalTransaction.Id));

            chainblock.Add(originalTransaction);
            ITransaction result = chainblock.GetById(originalTransaction.Id);
            Assert.That(result, Is.SameAs(originalTransaction));

            Assert.Throws<InvalidOperationException>(() => chainblock.GetById(originalTransaction.Id - 1));
            Assert.Throws<InvalidOperationException>(() => chainblock.GetById(originalTransaction.Id + 1));
        }
        [Test]
        public void Remove_ExistingId_ShouldRemoveTransactionWithGivenId()
        {
            ITransaction[] transactions = GetTransactions();

            for (int i = 0; i < transactions.Length; i++)
            {
                chainblock.Add(transactions[i]);
            }

            for (int i = 0; i < transactions.Length; i++)
            {
                chainblock.RemoveTransactionById(transactions[i].Id);

                Assert.IsFalse(chainblock.Contains(transactions[i].Id));

                AssertExist(transactions, i + 1, transactions.Length - 1);
            }
        }
        [Test]
        public void Remove_IdNotFound_ThrowInvalidOperationException()
        {
            ITransaction transaction = GetTransaction();

            Assert.Throws<InvalidOperationException>(
                () => chainblock.RemoveTransactionById(transaction.Id));

            chainblock.Add(transaction);

            Assert.Throws<InvalidOperationException>(
                () => chainblock.RemoveTransactionById(transaction.Id - 1));

            Assert.Throws<InvalidOperationException>(
                () => chainblock.RemoveTransactionById(transaction.Id + 1));
        }
        [Test]
        public void GetByStatus_ValidData_ShouldReturnTransactionsWithGivenStatus()
        {
            ITransaction[] transactions = GetTransactions();

            Dictionary<TransactionStatus, List<ITransaction>> transactionsByStatus = 
                new Dictionary<TransactionStatus, List<ITransaction>>();

            foreach (ITransaction transaction in transactions)
            {
                chainblock.Add(transaction);

                if (!transactionsByStatus.ContainsKey(transaction.Status))
                {
                    transactionsByStatus[transaction.Status] = new List<ITransaction>();
                }

                transactionsByStatus[transaction.Status].Add(transaction);

                foreach(var (status, expectedTransactions) in transactionsByStatus)
                {
                    AssertExist(expectedTransactions, chainblock.GetByTransactionStatus(status));
                }
            }
        }
        [Test]
        public void GetByStatus_ValidData_ShouldReturnTransactionsInCorrectOrder()
        {
            ITransaction[] transactions = GetTransactions();

            HashSet<TransactionStatus> usedStatuses = new HashSet<TransactionStatus>();

            foreach(ITransaction transaction in transactions)
            {
                chainblock.Add(transaction);
                usedStatuses.Add(transaction.Status);

                foreach(TransactionStatus status in usedStatuses)
                {
                    AssertOrder(chainblock.GetByTransactionStatus(status));
                }
            }
        }
        [Test]
        public void GetByStatus_NoTransactionsWithGivenStatus_ThrowInvalidOperationException()
        {
            ITransaction[] transactions = new ITransaction[allStatuses.Length];
            for (int i = 0; i < allStatuses.Length; i++)
            {
                transactions[i] = GetTransaction(SetupId(i + 1), SetupStatus(allStatuses[i]));
                chainblock.Add(transactions[i]);
            }

            foreach(ITransaction transaction in transactions)
            {
                chainblock.RemoveTransactionById(transaction.Id);

                Assert.Throws<InvalidOperationException>(()
                    => chainblock.GetByTransactionStatus(transaction.Status));

                chainblock.Add(transaction);
            }
        }
        [TestCase(true)]
        [TestCase(false)]
        public void GetAllSendersWithTransactionStatus_ValidData_ShouldReturnExpectedSenders
            (bool allowRepetition)
        {
            Func<ITransaction[], int, IEnumerable<Action<Mock<ITransaction>>>> generationFunc = null;
            if (allowRepetition)
            {
                generationFunc = (t, i) =>
                {
                    if (i <= 0)
                    {
                        return Enumerable.Empty<Action<Mock<ITransaction>>>();
                    }

                    return new[] { SetupOptionally(SetupSender(t[i - 1].From)) };
                };
            }
            ITransaction[] transactions = GetTransactions(generationFunc);

            Dictionary<TransactionStatus, Dictionary<string, int>> sendersByStatus =
                new Dictionary<TransactionStatus, Dictionary<string, int>>();

            foreach(ITransaction transaction in transactions)
            {
                chainblock.Add(transaction);

                if(!sendersByStatus.ContainsKey(transaction.Status))
                {
                    sendersByStatus[transaction.Status] = new Dictionary<string, int>();
                }
                if (!sendersByStatus[transaction.Status].ContainsKey(transaction.From))
                {
                    sendersByStatus[transaction.Status][transaction.From] = 0;
                }

                sendersByStatus[transaction.Status][transaction.From]++;

                foreach(var (status, expectedSenders) in sendersByStatus)
                {
                    Dictionary<string, int> actualSenders = new Dictionary<string, int>();

                    foreach(string sender in chainblock.GetAllSendersWithTransactionStatus(status))
                    {
                        if(!actualSenders.ContainsKey(sender))
                        {
                            actualSenders[sender] = 0;
                        }

                        actualSenders[sender]++;
                    }

                    Assert.AreEqual(expectedSenders, actualSenders);
                }
            }
        }
        private void AssertExist(ITransaction[] transactions, int start, int end)
        {
            for(int i = start; i <= end; i++)
            {
                Assert.AreEqual(transactions[i], chainblock.GetById(transactions[i].Id));
            }
        }
        private void AssertExist(IReadOnlyCollection<ITransaction> expectedTransactions, 
            IEnumerable<ITransaction> actualTransactions)
        {
            IDictionary<int, ITransaction> actualTransactionsById =
                 actualTransactions.ToDictionary(x => x.Id);

            Assert.AreEqual(expectedTransactions.Count, actualTransactionsById.Count);

            foreach (ITransaction expectedTransaction in expectedTransactions)
            {
                Assert.IsTrue(actualTransactionsById.ContainsKey(expectedTransaction.Id));
                Assert.AreSame(expectedTransaction, actualTransactionsById[expectedTransaction.Id]);
            }
        }
        private void AssertOrder(IEnumerable<ITransaction> result)
        {
            ITransaction[] orderedResult = result.ToArray();

            for(int i = 1; i < orderedResult.Length; i++)
            {
                int amountComparison = orderedResult[i].Amount.CompareTo(orderedResult[i - 1].Amount);
                Assert.IsTrue(amountComparison <= 0);

                if (amountComparison == 0)
                {
                    Assert.IsTrue(orderedResult[i].Id > orderedResult[i - 1].Id);
                }
            }
        }
        private static Action<Mock<ITransaction>> SetupId(int id)
        {
            return mock => mock.SetupGet(x => x.Id).Returns(id);
        }
        private static Action<Mock<ITransaction>> SetupStatus(TransactionStatus status)
        {
            return mock => mock.SetupGet(x => x.Status).Returns(status);
        }
        private static Action<Mock<ITransaction>> SetupOptionally(Action<Mock<ITransaction>> setupAction)
        {
            return mock =>
            {
                if(GenerateRandomBool())
                {
                    setupAction(mock);
                }
            };
        }
        private static Action<Mock<ITransaction>> SetupSender(string sender)
        {
            return mock => mock.SetupGet(x => x.From).Returns(sender);
        }
        private ITransaction[] GetTransactions
            (Func<ITransaction[], int,
                IEnumerable<Action<Mock<ITransaction>>>> generationFunc = null)
        {
            int transactionsCount = Random.Shared.Next(10, 20);
            ITransaction[] transactions = new ITransaction[transactionsCount];

            for (int i = 0; i < transactionsCount; i++)
            {
                IEnumerable<Action<Mock<ITransaction>>> additionalSetupActions = 
                    Enumerable.Empty<Action<Mock<ITransaction>>>();

                if(generationFunc is not null)
                {
                    additionalSetupActions = generationFunc(transactions, i);
                }

                Action<Mock<ITransaction>>[] allSetupActions = 
                    new[] { SetupId(i + 1) }
                    .Concat(additionalSetupActions)
                    .ToArray();
                transactions[i] = GetTransaction(allSetupActions);
            }
            Shuffle(transactions);

            return transactions;
        }
        private ITransaction GetTransaction(params Action<Mock<ITransaction>>[] setupActions)
        {
            Mock<ITransaction> transactionMock = new Mock<ITransaction>();

            int randomId = Random.Shared.Next();
            transactionMock.SetupGet(x => x.Id).Returns(randomId);

            double randomAmount = Random.Shared.NextDouble();
            transactionMock.SetupGet(x => x.Amount).Returns(randomAmount);

            string randomFrom = GenerateRandomString();
            transactionMock.SetupGet(x => x.From).Returns(randomFrom);

            string randomTo = GenerateRandomString();
            transactionMock.SetupGet(x => x.To).Returns(randomTo);

            int randomStatusIndex = Random.Shared.Next(0, allStatuses.Length);
            TransactionStatus randomStatus = allStatuses[randomStatusIndex];
            transactionMock.SetupGet(x => x.Status).Returns(randomStatus);

            foreach (Action<Mock<ITransaction>> action in setupActions)
            {
                action(transactionMock);
            }

            return transactionMock.Object;
        }
        private static void Shuffle<T>(T[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                int swapIndex = Random.Shared.Next(i, array.Length);

                if(swapIndex != i)
                {
                    T temp = array[i];
                    array[i] = array[swapIndex];
                    array[swapIndex] = temp;
                }
            }
        }
        private static string GenerateRandomString()
        {
            int randomTextLength = Random.Shared.Next(5, 50);
            return GenerateRandomString(randomTextLength);
        }
        private static string GenerateRandomString(int length)
        {
            char[] symbols = new char[length];
            for (int i = 0; i < length; i++)
            {
                int randomLetterIndex = Random.Shared.Next(26);
                symbols[i] = (char)('a' + randomLetterIndex);
            }

            return new string(symbols);
        }
        private static bool GenerateRandomBool(int successPercentage = 50)
        {
            Assert.IsTrue(successPercentage > 0 && successPercentage < 100);

            return Random.Shared.Next(0, 100) < successPercentage;
        }
    }
}