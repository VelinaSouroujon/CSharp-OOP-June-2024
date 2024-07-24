namespace CarManager.Tests
{
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class CarManagerTests
    {
        private string make;
        private string model;
        private double fuelConsumption;
        private double fuelCapacity;

        [SetUp]
        public void SetUp()
        {
            make = GenerateRandomString();
            model = GenerateRandomString();
            fuelConsumption = Random.Shared.NextDouble();
            fuelCapacity = fuelConsumption * 5;
        }

        [Test]
        public void Initialize_ValidData_CreateCarSuccessfully()
        {
            Car car = CreateCar();

            Assert.AreEqual(make, car.Make);
            Assert.AreEqual(model, car.Model);
            Assert.AreEqual(fuelConsumption, car.FuelConsumption);
            Assert.AreEqual(fuelCapacity, car.FuelCapacity);
            Assert.AreEqual(0, car.FuelAmount);
        }
        [TestCase("")]
        [TestCase(null)]
        public void Initialize_MakeIsInvalid_ThrowArgumentException(string make)
        {
            Assert.Throws<ArgumentException>(() => new Car(make, model, fuelConsumption, fuelCapacity));
        }
        [TestCase("")]
        [TestCase(null)]
        public void Initialize_ModelIsInvalid_ThrowArgumentException(string model)
        {
            Assert.Throws<ArgumentException>(() => new Car(make, model, fuelConsumption, fuelCapacity));
        }
        [TestCase(0)]
        [TestCase(-0.00001)]
        [TestCase(-1)]
        [TestCase(-12)]
        public void Initialize_FuelConsumptionIsZeroOrNegative_ThrowArgumentException(double fuelConsumption)
        {
            Assert.Throws<ArgumentException>(() => new Car(make, model, fuelConsumption, fuelCapacity));
        }
        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(-102)]
        public void Initialize_FuelCapacityIsZeroOrNegative_ThorArgumentException(double fuelCapacity)
        {
            Assert.Throws<ArgumentException>(() => new Car(make, model, fuelConsumption, fuelCapacity));
        }
        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(-0.53)]
        [TestCase(-5)]
        public void Refuel_ZeroOrNegativeValue_ThrowArgumentException(double fuelToRefuel)
        {
            Car car = CreateCar();

            Assert.Throws<ArgumentException>(() => car.Refuel(fuelToRefuel));
        }
        [TestCase(0.25)]
        [TestCase(0.5)]
        [TestCase(0.99)]
        [TestCase(1)]
        public void Refuel_ValidDataFuelCapacityIsNotExceeded_IncreaseFuelAmount(double fillPercentage)
        {
            Car car = CreateCar();

            double fuelToRefuel = fuelCapacity * fillPercentage;
            car.Refuel(fuelToRefuel);

            Assert.AreEqual(fuelToRefuel, car.FuelAmount);
        }
        [TestCase(1.0000001)]
        [TestCase(1.056)]
        [TestCase(29)]
        [TestCase(7)]
        public void Refuel_FuelCapacityIsExceeded_FuelAmountIsEqualToFuelCapacity(double fillPercentage)
        {
            Car car = CreateCar();

            double fuelToRefuel = fuelCapacity * fillPercentage;
            car.Refuel(fuelToRefuel);

            Assert.AreEqual(car.FuelCapacity, car.FuelAmount);
        }
        [Test]
        public void Drive_FuelAmountNotEnough_ThorwInvalidOperationException()
        {
            Car car = CreateCar();

            car.Refuel(car.FuelCapacity);

            double fuelNeeded = car.FuelAmount / car.FuelConsumption * 100;

            Assert.Throws<InvalidOperationException>(() => car.Drive(fuelNeeded + 0.1));
        }
        [Test]
        public void Drive_ValidData_ShouldDecreaseFuelAmount()
        {
            Car car = CreateCar();

            car.Refuel(car.FuelCapacity);
            double expectedFuelAmount = car.FuelCapacity - car.FuelConsumption;
            car.Drive(100);

            Assert.AreEqual(expectedFuelAmount, car.FuelAmount);
        }
        private Car CreateCar()
        {
            return new Car(make, model, fuelConsumption, fuelCapacity);
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
    }
}