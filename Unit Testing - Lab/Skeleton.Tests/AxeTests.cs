using NUnit.Framework;
using System;

namespace Skeleton.Tests
{
    [TestFixture]
    public class AxeTests
    {
        private const int InitialAttackPoints = 10;
        private const int InitialDurabilityPoints = 5;

        private Axe axe;
        private Dummy dummy;
        [SetUp]
        public void SetUp()
        {
            axe = new Axe(InitialAttackPoints, InitialDurabilityPoints);
            dummy = new Dummy(100, 100);
        }
        [Test]
        public void NewAxe_ShouldSetDataCorrectly()
        {
            Assert.AreEqual(InitialAttackPoints, axe.AttackPoints);
            Assert.AreEqual(InitialDurabilityPoints, axe.DurabilityPoints);
        }

        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(-100)]
        public void Attack_DurabilityPointsNotPositive_ShouldThrowError(int durabilityPoints)
        {
            axe = new Axe(InitialAttackPoints, durabilityPoints);

            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() => axe.Attack(dummy));
            Assert.AreEqual("Axe is broken.", ex.Message);
        }

        [TestCase(1, 0)]
        [TestCase(10, 9)]
        public void Attack_ValidData_DurabilityShouldDecreaseByOnePoint(int initialDurabilityPoints, int expectedDurabilityPoints)
        {
            axe = new Axe(InitialAttackPoints, initialDurabilityPoints);

            axe.Attack(dummy);

            Assert.AreEqual(expectedDurabilityPoints, axe.DurabilityPoints);
        }
    }
}