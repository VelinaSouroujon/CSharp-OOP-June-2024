namespace FightingArena.Tests
{
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    [TestFixture]
    public class ArenaTests
    {
        private Arena arena;

        [SetUp]
        public void SetUp()
        {
            arena = new Arena();
        }

        [Test]
        public void Initialize_ShouldCreateEmptyArena()
        {
            Assert.AreEqual(0, arena.Count);
            Assert.AreEqual(new List<Warrior>(), arena.Warriors);
        }
        [Test]
        public void Enroll_ValidData_ShouldAddWarrior()
        {
            int count = 10;

            List<Warrior> expectedWarriors = new List<Warrior>();

            for (int i = 0; i < count; i++)
            {
                Warrior warriorToAdd = new Warrior(i.ToString(), i + 50, i + 62);

                expectedWarriors.Add(warriorToAdd);
                arena.Enroll(warriorToAdd);
            }

            Assert.AreEqual(expectedWarriors, arena.Warriors);
            Assert.AreEqual(count, arena.Count);
        }
        [Test]
        public void Enroll_DublicateName_ThrowInvalidOperationException()
        {
            Warrior warrior = GetWarrior();

            arena.Enroll(warrior);

            Assert.Throws<InvalidOperationException>(() => arena.Enroll(new Warrior(warrior.Name, 90, 82)));
        }
        [TestCase(null)]
        [TestCase("")]
        [TestCase("0InvalidName")]
        public void Fight_MissingAttacker_ThrowInvalidOperationException(string attackerName)
        {
            PopulateArenaWithWarriors();

            string defernderName = arena.Warriors.First().Name;

            InvalidOperationException ex = 
                Assert.Throws<InvalidOperationException>(() => arena.Fight(attackerName, defernderName));

            Assert.AreEqual($"There is no fighter with name {attackerName} enrolled for the fights!", ex.Message);
        }
        [TestCase(null)]
        [TestCase("")]
        [TestCase("0InvalidName")]
        public void Fight_MissingDefender_ThrowInvalidOperationException(string defenderName)
        {
            PopulateArenaWithWarriors();

            string attackerName = arena.Warriors.First().Name;

            InvalidOperationException ex = 
                Assert.Throws<InvalidOperationException>(() => arena.Fight(attackerName, defenderName));

            Assert.AreEqual($"There is no fighter with name {defenderName} enrolled for the fights!", ex.Message);
        }
        [Test]
        public void Fight_ValidData_AttackerShouldAttackDefender()
        {
            Warrior attacker = new Warrior("warrior1", 54, 78);
            Warrior defender = new Warrior("warrior2", 47, 91);

            int expectedDefenderHP = defender.HP - attacker.Damage;
            int expectedAttackerHP = attacker.HP - defender.Damage;

            arena.Enroll(attacker);
            arena.Enroll(defender);

            arena.Fight(attacker.Name, defender.Name);

            Assert.AreEqual(expectedDefenderHP, defender.HP);
            Assert.AreEqual(expectedAttackerHP, attacker.HP);
        }
        private Warrior GetWarrior()
        {
            return new Warrior("Hero", 70, 100);
        }
        private void PopulateArenaWithWarriors()
        {
            for (int i = 0; i < 10; i++)
            {
                Warrior warriorToAdd = new Warrior(i.ToString(), i + 50, i + 62);

                arena.Enroll(warriorToAdd);
            }
        }
    }
}
