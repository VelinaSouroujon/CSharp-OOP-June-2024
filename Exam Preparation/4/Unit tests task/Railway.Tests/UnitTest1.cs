namespace Railway.Tests
{
    using NUnit.Framework;
    using System;
    using System.Linq;
    using System.Collections.Generic;
    public class Tests
    {
        private RailwayStation station;
        [SetUp]
        public void Setup()
        {
            station = new RailwayStation("Varna");
        }

        [Test]
        public void Initialize_ValidData_ShouldWorkCorrectly()
        {
            string name = "Sofia";
            station = new RailwayStation(name);

            Assert.AreEqual(name, station.Name);
            Assert.IsTrue(station.ArrivalTrains.Count == 0);
            Assert.IsTrue(station.DepartureTrains.Count == 0);
        }
        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        [TestCase("                       ")]
        public void Initialize_InvalidName_ThrowException(string invalidName)
        {
            Assert.Throws<ArgumentException>(() => station = new RailwayStation(invalidName));
        }
        [Test]
        public void NewArrivalOnBoard_ValidData_ShouldWorkCorrectly()
        {
            int n = 15;
            Queue<string> expected = new Queue<string>();

            for (int i = 0; i < n; i++)
            {
                string trainInfo = $"Train{i}";

                expected.Enqueue(trainInfo);
                station.NewArrivalOnBoard(trainInfo);

                CollectionAssert.AreEqual(expected, station.ArrivalTrains);
            }
        }
        [Test]
        public void TrainHasArrived_ThereAreOtherTrainsToArrive()
        {
            FillArrivalTrains();
            string trainInfo = station.ArrivalTrains.Peek() + "2";

            string expected = $"There are other trains to arrive before {trainInfo}.";
            string actual = station.TrainHasArrived(trainInfo);

            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void TrainHasArrived_ArriveSuccessfully()
        {
            FillArrivalTrains();

            Queue<string> expectedDepartureTrains = new Queue<string>();

            int n = station.ArrivalTrains.Count;
            for (int i = 0;i < n;i++)
            {
                string trainInfo = station.ArrivalTrains.Peek();

                expectedDepartureTrains.Enqueue(trainInfo);
                string expectedResult = $"{trainInfo} is on the platform and will leave in 5 minutes.";
                string actualResult = station.TrainHasArrived(trainInfo);

                CollectionAssert.AreEqual(expectedDepartureTrains, station.DepartureTrains);
                Assert.AreEqual(expectedResult, actualResult);
            }
        }
        [Test]
        public void TrainHasLeft_LeavesSuccessfully_ShouldReturnTrue()
        {
            FillDepartureTrains();
            Queue<string> expectedDepartureTrains = new Queue<string>(station.DepartureTrains);

            int n = station.DepartureTrains.Count;
            for(int i = 0; i < n;i++)
            {
                bool actualResult = station.TrainHasLeft(station.DepartureTrains.Peek());
                Assert.IsTrue(actualResult);

                CollectionAssert.AreEqual
                    (expectedDepartureTrains.Skip(i + 1), station.DepartureTrains);
            }
        }
        [Test]
        public void TrainHasLeft_LeavesUnsuccessfully_ShouldReturnFalse()
        {
            FillDepartureTrains();
            Queue<string> expectedDepartureTrains = new Queue<string>(station.DepartureTrains);

            for(int i = 0; i < 20; i++)
            {
                bool actualResult = station.TrainHasLeft(station.DepartureTrains.Peek() + "2");
                Assert.IsFalse(actualResult);

                CollectionAssert.AreEqual(expectedDepartureTrains, station.DepartureTrains);
            }
        }
        private void FillArrivalTrains()
        {
            int n = 15;
            for (int i = 0;i < n;i++)
            {
                station.NewArrivalOnBoard($"Train{i}");
            }
        }
        private void FillDepartureTrains()
        {
            FillArrivalTrains();

            int n = station.ArrivalTrains.Count;
            for(int i = 0; i < n;i++)
            {
                station.TrainHasArrived(station.ArrivalTrains.Peek());
            }
        }
    }
}