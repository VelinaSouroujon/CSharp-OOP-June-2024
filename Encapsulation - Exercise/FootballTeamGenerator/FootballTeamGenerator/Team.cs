using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballTeamGenerator
{
    public class Team
    {
        private string name;
        private Dictionary<string, Player> players;

        public Team(string name)
        {
            Name = name;
            players = new Dictionary<string, Player>();
        }

        public string Name
        {
            get => name;

            private set
            {
                Validator.ValidateName(value);

                name = value;
            }
        }

        public int Rating => players.Count == 0 ? 0 :
                                (int) Math.Round(players.Values.Average(x => x.Stats.Average(y => y.Value)));

        public void AddPlayer(Player player)
        {
            players.Add(player.Name, player);
        }
        public void RemovePlayer(string playerName)
        {
            if (!players.Remove(playerName))
            {
                throw new ArgumentException($"Player {playerName} is not in {Name} team.");
            }
        }
    }
}
