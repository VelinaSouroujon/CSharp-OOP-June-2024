using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raiding
{
    public class Engine : IEngine
    {
        private const string InvalidHeroMessage = "Invalid hero!";

        private IReader reader;
        private IWriter writer;

        private List<BaseHero> heroes;

        public Engine(IReader reader, IWriter writer)
        {
            this.reader = reader;
            this.writer = writer;

            heroes = new List<BaseHero>();
        }

        public void Run()
        {
            int n = int.Parse(reader.ReadLine());

            for (int i = 0; i < n; i++)
            {
                try
                {
                    string heroName = reader.ReadLine();
                    string heroType = reader.ReadLine();

                    BaseHero hero = GetHero(heroName, heroType);
                    heroes.Add(hero);
                }
                catch (Exception ex)
                {
                    i--;
                    writer.WriteLine(ex.Message);
                }
            }

            int bossHealth = int.Parse(reader.ReadLine());

            foreach(BaseHero hero in heroes)
            {
                writer.WriteLine(hero.CastAbility());
                bossHealth -= hero.Power;
            }

            if(bossHealth <= 0)
            {
                writer.WriteLine("Victory!");
            }
            else
            {
                writer.WriteLine("Defeat...");
            }
        }

        private BaseHero GetHero(string name, string type)
        {
            switch (type.ToLower())
            {
                case "druid":
                    return new Druid(name);

                case "paladin":
                    return new Paladin(name);

                case "rogue":
                    return new Rogue(name);

                case "warrior":
                    return new Warrior(name);

                default:
                    throw new ArgumentException(InvalidHeroMessage);
            }
        }
    }
}
