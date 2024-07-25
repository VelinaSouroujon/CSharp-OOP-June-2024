namespace FightingArena.Tests
{
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class WarriorTests
    {
        private const int MIN_ATTACK_HP = 30;

        private string name;
        private int damage;
        private int hp;

        [SetUp]
        public void SetUp()
        {
            name = "Batman";
            damage = 27;
            hp = MIN_ATTACK_HP + 1;
        }
        [Test]
        public void Initialize_ValidData_CreateWarrior()
        {
            Warrior warrior = new Warrior(name, damage, hp);

            Assert.AreEqual(name, warrior.Name);
            Assert.AreEqual(damage, warrior.Damage);
            Assert.AreEqual(hp, warrior.HP);
        }
        [TestCase("")]
        [TestCase(null)]
        public void Initialize_InvalidName_ThrowArgumentException(string inputName)
        {
            Assert.Throws<ArgumentException>(() => new Warrior(inputName, damage, hp));
        }
        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(-6)]
        public void Initialize_DamageIsZeroOrNegative_ThrowArgumentException(int inputDamage)
        {
            Assert.Throws<ArgumentException>(() => new Warrior(name, inputDamage, hp));
        }
        [TestCase(-1)]
        [TestCase(-11)]
        [TestCase(-902)]
        public void Initialize_HpIsNegative_ThrowArgumentException(int inputHP)
        {
            Assert.Throws<ArgumentException>(() => new Warrior(name, damage, inputHP));
        }
        [TestCase(MIN_ATTACK_HP)]
        [TestCase(MIN_ATTACK_HP - 1)]
        [TestCase(MIN_ATTACK_HP - 2)]
        [TestCase(0)]
        public void Attack_HpIsTooLow_ThrowInvalidOperationException(int inputHP)
        {
            Warrior warrior = new Warrior(name, damage, inputHP);

            Assert.Throws<InvalidOperationException>(() =>
            warrior.Attack(new Warrior("Hero", hp - 2, MIN_ATTACK_HP + 10)));
        }
        [TestCase(MIN_ATTACK_HP)]
        [TestCase(MIN_ATTACK_HP - 1)]
        [TestCase(0)]
        public void Attack_EnemyWarriorHasTooLowHp_ThrowInvalidOperationException(int enemyWarriorHP)
        {
            Warrior warrior = new Warrior(name, damage, hp);

            Assert.Throws<InvalidOperationException>(() =>
            warrior.Attack(new Warrior("Hero", hp - 2, enemyWarriorHP)));
        }

        [TestCase(MIN_ATTACK_HP + 20, MIN_ATTACK_HP + 150)]
        [TestCase(MIN_ATTACK_HP + 1, MIN_ATTACK_HP + 2)]
        [TestCase(MIN_ATTACK_HP + 4, MIN_ATTACK_HP + 6)]
        public void Attack_EnemyWarriorHasBiggerDamageThanMyHP_ThrowInvalidOperationException
            (int inputHP, int inputEnemyWarriorDamage)
        {
            Warrior warrior = new Warrior(name, damage, inputHP);

            Assert.Throws<InvalidOperationException>(() =>
            warrior.Attack(new Warrior("Hero", inputEnemyWarriorDamage, MIN_ATTACK_HP + 3)));
        }
        [TestCase(MIN_ATTACK_HP + 1, MIN_ATTACK_HP + 1)]
        [TestCase(MIN_ATTACK_HP + 30,MIN_ATTACK_HP + 20)]
        public void Attack_ValidData_DecreaseHP(int inputHP, int inputEnemyWarriorDamage)
        {
            Warrior warrior = new Warrior(name, damage, inputHP);

            warrior.Attack(new Warrior(name, inputEnemyWarriorDamage, MIN_ATTACK_HP + 30));

            Assert.AreEqual(inputHP - inputEnemyWarriorDamage, warrior.HP);
        }
        [TestCase(MIN_ATTACK_HP + 2, MIN_ATTACK_HP + 1)]
        [TestCase(MIN_ATTACK_HP + 10, MIN_ATTACK_HP + 9)]
        [TestCase(MIN_ATTACK_HP + 102, MIN_ATTACK_HP + 3)]
        public void Attack_MyWarriorHasMoreDamageThanEnemyWorriorHP_EnemyWarriorHPIsSetToZero
            (int inputDamage, int inputEnemyWarriorHP)
        {
            Warrior warrior = new Warrior(name, inputDamage, hp);

            Warrior enemyWarrior = new Warrior("Hero", warrior.HP - 2, inputEnemyWarriorHP);

            warrior.Attack(enemyWarrior);

            Assert.AreEqual(0, enemyWarrior.HP);
        }
        [TestCase(50, 60)]
        [TestCase(70, 70)]
        [TestCase(100, 101)]
        public void Attack_ValidData_DecreaseEnemyWarriorHP
            (int inputDamage, int inputEnemyWarriorHP)
        {
            Warrior warrior = new Warrior(name, inputDamage, hp);

            Warrior enemyWarrior = new Warrior("Hero", warrior.HP - 1, inputEnemyWarriorHP);

            warrior.Attack(enemyWarrior);

            Assert.AreEqual(inputEnemyWarriorHP - inputDamage, enemyWarrior.HP);
        }
    }
}