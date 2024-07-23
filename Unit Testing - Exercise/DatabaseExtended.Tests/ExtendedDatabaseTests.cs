namespace DatabaseExtended.Tests
{
    using ExtendedDatabase;
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    [TestFixture]
    public class ExtendedDatabaseTests
    {
        private const int MaxCount = 16;

        [TestCase(0)]
        [TestCase(8)]
        [TestCase(3)]
        [TestCase(MaxCount)]
        public void Initialize_ValidData_ShouldCreateDatabase(int elementsCount)
        {
            Person[] originalPeople = GenerateRandomPeople(elementsCount);

            Database database = new Database(originalPeople);

            Assert.AreEqual(elementsCount, database.Count);

            for (int i = 0; i < originalPeople.Length; i++)
            {
                AssertIsFound(database, originalPeople[i]);
            }
        }
        [TestCase(MaxCount + 1)]
        [TestCase(MaxCount * 2)]
        [TestCase(MaxCount * 5)]
        public void Initialize_CountIsTooBig_ThrowArgumentException(int elementsCount)
        {
            Database database;

            Person[] people = GenerateRandomPeople(elementsCount);

            Assert.Throws<ArgumentException>(() => database = new Database(people));
        }
        [Test]
        public void Add_ValidData_ShouldAddPerson()
        {
            Database database = new Database();

            for (int i = 0; i < MaxCount; i++)
            {
                Person personToAdd = GenerateRandomPerson();

                database.Add(personToAdd);

                Assert.AreEqual(i + 1, database.Count);
                AssertIsFound(database, personToAdd);
            }
        }

        [Test]
        public void Add_CountIsTooBig_ThrowInvalidOperationException()
        {
            Person[] people = GenerateRandomPeople(MaxCount);

            Database database = new Database(people);

            Assert.Throws<InvalidOperationException>(() => database.Add(GenerateRandomPerson()));
        }

        [Test]
        public void Add_PersonWithSameId_ThrowInvalidOperationException()
        {
            Database database = new Database();

            Person person = GenerateRandomPerson();
            database.Add(person);

            Person personDuplicateId = new Person(person.Id, GenerateRandomString());

            Assert.Throws<InvalidOperationException>(() => database.Add(personDuplicateId));
        }
        [Test]
        public void Add_PersonWithSameUsername_ThrowInvalidOperationException()
        {
            Database database = new Database();

            Person person = GenerateRandomPerson();
            database.Add(person);

            Person personDuplicateUsername = new Person(person.Id + 1, person.UserName);
            Assert.Throws<InvalidOperationException>(() => database.Add(personDuplicateUsername));
        }
        [Test]
        public void Remove_CountIsPositive_ShouldRemoveLastPerson()
        {
            Person[] people = GenerateRandomPeople(MaxCount);
            Database database = new Database(people);

            for (int i = 0; i < MaxCount; i++)
            {
                database.Remove();

                int currentCount = MaxCount - 1 - i;
                Assert.AreEqual(currentCount, database.Count);
                
                for (int j = 0; j < currentCount; j++)
                {
                    AssertIsFound(database, people[j]);
                }
            }
        }
        [Test]
        public void Remove_CountIsZero_ThrowInvalidOperationException()
        {
            Database database = new Database();

            Assert.Throws<InvalidOperationException>(() => database.Remove());
        }
        [Test]
        public void FindByUsername_NullValue_ThrowArgumentNullException()
        {
            Database database = new Database();

            Assert.Throws<ArgumentNullException>(() => database.FindByUsername(null));
        }
        [Test]
        public void FindByUsername_UsernameNotFound_ThrowInvalidOperationException()
        {
            Person person = GenerateRandomPerson();
            Database database = new Database(person);

            Assert.Throws<InvalidOperationException>(() => 
            database.FindByUsername($"{person.UserName} 2"));
        }
        [TestCase(-1)]
        [TestCase(-3)]
        [TestCase(-480)]
        public void FindById_IdIsNegative_ThrowArgumentOutOfRangeException(int id)
        {
            Database database = new Database();

            Assert.Throws<ArgumentOutOfRangeException>(() => database.FindById(id));
        }
        [Test]
        public void FindById_IdNotFound_ThrowInvalidOperationException()
        {
            Person person = GenerateRandomPerson();
            Database database = new Database(person);

            Assert.Throws<InvalidOperationException>(() => database.FindById(person.Id + 1));
        }
        private static void AssertIsFound(Database database, Person person)
        {
            Person matchByUsername = database.FindByUsername(person.UserName);
            Assert.AreSame(person, matchByUsername);

            Person matchById = database.FindById(person.Id);
            Assert.AreSame(person, matchById);
        }
        private static Person GenerateRandomPerson()
        {
            return new Person(Random.Shared.NextInt64(), GenerateRandomString());
        }
        private static Person[] GenerateRandomPeople(int length)
        {
            Person[] result = new Person[length];
            for (int i = 0; i < length; i++)
            {
                result[i] = GenerateRandomPerson();
            }

            return result;
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
                symbols[i] = (char) ('a' + randomLetterIndex);
            }

            return new string(symbols);
        }
    }
}