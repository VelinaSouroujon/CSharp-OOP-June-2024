namespace Database.Tests
{
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    [TestFixture]
    public class DatabaseTests
    {
        private const int MaxCount = 16;

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(MaxCount)]
        [TestCase(10)]
        public void Initialization_ValidData_ShouldCreateDatabaseObject(int elementsCount)
        {
            int[] originalElements = GenerateRandomIntegers(elementsCount);

            Database database = new Database(originalElements);

            Assert.AreEqual(elementsCount, database.Count);
            Assert.AreEqual(originalElements, database.Fetch());
        }
        [TestCase(MaxCount + 1)]
        [TestCase(25)]
        [TestCase(130)]
        public void Initialization_CountIsBiggerThanMaxCount_ThrowInvalidOperationException(int elementsCount)
        {
            Database database;

            int[] elements = GenerateRandomIntegers(elementsCount);

            Assert.Throws<InvalidOperationException>(() => database = new Database(elements));
        }
        [Test]
        public void Add_CountIsBiggerThanMaxCount_ThrowInvalidOperationException()
        {
            int[] elements = GenerateRandomIntegers(MaxCount);

            Database database = new Database(elements);

            Assert.Throws<InvalidOperationException>(() => database.Add(Random.Shared.Next()));
        }
        [Test]
        public void Add_ValidData_ShouldAddNumberAndIncreaseCount()
        {
            Database database = new Database();

            List<int> addedNumbers = new List<int>();

            for (int i = 0; i < MaxCount; i++)
            {
                int numberToAdd = Random.Shared.Next();
                database.Add(numberToAdd);
                addedNumbers.Add(numberToAdd);

                Assert.AreEqual(i + 1, database.Count);
                Assert.AreEqual(addedNumbers, database.Fetch());
            }
        }

        [Test]
        public void Remove_CountIsZero_ThrowInvalidOperationException()
        {
            Database database = new Database();

            Assert.Throws<InvalidOperationException>(() => database.Remove());
        }
        [Test]
        public void Remove_CountIsPositive_RemoveElementAndDecreaseCount()
        {
            int[] elements = GenerateRandomIntegers(MaxCount);

            Database database = new Database(elements);

            for (int i = 0; i < elements.Length; i++)
            {
                database.Remove();

                int expectedCount = elements.Length - 1 - i;

                Assert.AreEqual(expectedCount, database.Count);
                Assert.AreEqual(elements.Take(expectedCount), database.Fetch());
            }
        }
        private static int[] GenerateRandomIntegers(int length)
        {
            int[] result = new int[length];
            for(int i = 0;i < result.Length;i++)
            {
                result[i] = Random.Shared.Next();
            }

            return result;
        }
    }
}
