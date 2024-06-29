using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballTeamGenerator
{
    public class Player
    {
        private const int MinStatsValue = 0;
        private const int MaxStatsValue = 100;
        private const string InvalidStatsMessage = "{0} should be between {1} and {2}.";

        private string name;
        private Dictionary<string, int> stats;

        public Player(string name, IList<int> stats)
        {
            Name = name;

            InitializeDictionaryWithStats();
            FillStats(stats);
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
        public IReadOnlyDictionary<string, int> Stats => stats;
        private void InitializeDictionaryWithStats()
        {
            stats = new Dictionary<string, int>()
            {
                { "Endurance", 0 },
                { "Sprint", 0 },
                { "Dribble", 0 },
                { "Passing", 0 },
                { "Shooting", 0 }
            };
        }

        private void FillStats(IList<int> input)
        {
            int index = 0;

            foreach(var kvp in stats)
            {
                string statsName = kvp.Key;
                int statsValue = input[index];

                ValidateStats(statsName, statsValue);

                stats[statsName] = statsValue;

                index++;
            }
        }
        private void ValidateStats(string statsName, int value)
        {
            if(value < MinStatsValue || value > MaxStatsValue)
            {
                throw new ArgumentException(string.Format(InvalidStatsMessage, statsName, MinStatsValue, MaxStatsValue));
            }
        }
    }
}
