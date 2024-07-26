using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;

namespace FakeAxeAndDummy.Tests
{
    public class HeroTests
    {
        private Mock<IWeapon> mockWeapon;
        private Hero hero;

        [SetUp]
        public void SetUp()
        {
            mockWeapon = new Mock<IWeapon>();
            hero = new Hero("MyHero", mockWeapon.Object);
        }

        [Test]
        public void Attack_ValidData_GainExperience()
        {
            int targetExperience = 30;

            Mock<ITarget> mockTarget = new Mock<ITarget>();

            mockTarget.Setup(x => x.IsDead()).Returns(true);
            mockTarget.Setup(x => x.GiveExperience()).Returns(targetExperience);

            hero.Attack(mockTarget.Object);

            Assert.AreEqual(targetExperience, hero.Experience);
        }
    }
}
