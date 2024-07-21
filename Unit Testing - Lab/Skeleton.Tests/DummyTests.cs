using NUnit.Framework;
using System;

namespace Skeleton.Tests
{
    [TestFixture]
    public class DummyTests
    {
        private const int InitialHealth = 100;
        private const int InitialExperience = 100;

        [TestCase(0, 20)]
        [TestCase(-1, 0)]
        [TestCase(-9999, 100)]
        public void GiveExperience_WhenIsDead_ShouldReturnExperience(int health, int experience)
        {
            Dummy dummy = new Dummy(health, experience);

            int actualExperience = dummy.GiveExperience();

            Assert.AreEqual(experience, actualExperience);
        }
        [TestCase(2, 100)]
        [TestCase(1, 20)]
        [TestCase(45, 1)]
        [TestCase(200, 627)]
        public void GiveExperience_WhenIsNotDead_ThrowInvalidOperationException(int health, int experience)
        {
            Dummy dummy = new Dummy(health, experience);

            Assert.Throws<InvalidOperationException>(() => dummy.GiveExperience());
        }
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(25)]
        [TestCase(999)]
        public void IsDead_HealthPositive_ShoultReturnFalse(int health)
        {
            Dummy dummy = new Dummy(health, InitialExperience);

            Assert.AreEqual(false, dummy.IsDead());
        }
        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(-6)]
        [TestCase(-34)]
        public void IsDead_HealthNegativeOrZero_ShouldReturnTrue(int health)
        {
            Dummy dummy = new Dummy(health, InitialExperience);

            Assert.AreEqual(true, dummy.IsDead());
        }
        [TestCase(0, 0)]
        [TestCase(-1, 9)]
        [TestCase(-10, 1)]
        [TestCase(-86, 987)]
        public void TakeAttack_WhenDead_ThrowInvalidOperationException(int health, int attackPoints)
        {
            Dummy dummy = new Dummy(health, InitialExperience);

            Assert.Throws<InvalidOperationException>(() => dummy.TakeAttack(attackPoints));
        }
        [TestCase(90, 120)]
        [TestCase(1, 0)]
        [TestCase(10, 3)]
        [TestCase(2, 99999)]
        [TestCase(50, 86)]
        public void TakeAttack_WhenNotDead_LoseHealth(int health, int attackPoints)
        {
            Dummy dummy = new Dummy(health, InitialExperience);

            dummy.TakeAttack(attackPoints);
            int expectedHealth = health - attackPoints;

            Assert.AreEqual(expectedHealth, dummy.Health);
        }
    }
}