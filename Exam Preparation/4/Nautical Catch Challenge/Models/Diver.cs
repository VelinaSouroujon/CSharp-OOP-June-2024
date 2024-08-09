using NauticalCatchChallenge.Models.Contracts;
using NauticalCatchChallenge.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NauticalCatchChallenge.Models
{
    public abstract class Diver : IDiver
    {
        private string name;
        private int oxygenLevel;
        private double competitionPoints;

        private ICollection<string> catchedFish;

        protected Diver(string name, int oxygenLevel)
        {
            Name = name;
            OxygenLevel = oxygenLevel;

            catchedFish = new List<string>();
            Catch = (IReadOnlyCollection<string>)catchedFish;
        }

        public string Name
        {
            get => name;

            private set
            {
                if(string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.DiversNameNull);
                }
                name = value;
            }
        }

        public int OxygenLevel
        {
            get => oxygenLevel;

            protected set
            {
                if(value <= 0)
                {
                    oxygenLevel = 0;
                    HasHealthIssues = true;
                }
                else
                {
                    oxygenLevel = value;
                }
            }
        }

        public IReadOnlyCollection<string> Catch { get; }

        public double CompetitionPoints
        {
            get => Math.Round(competitionPoints, 1);

            private set
            {
                competitionPoints = value;
            }
        }

        public bool HasHealthIssues { get; private set; }
        protected abstract double DecreaseOxygenRate { get; }

        public void Hit(IFish fish)
        {
            OxygenLevel -= fish.TimeToCatch;
            catchedFish.Add(fish.Name);
            CompetitionPoints += fish.Points;
        }

        public void Miss(int TimeToCatch)
        {
            OxygenLevel -= (int)Math.Round(DecreaseOxygenRate * TimeToCatch, MidpointRounding.AwayFromZero);
        }

        public abstract void RenewOxy();

        public void UpdateHealthStatus()
        {
            if (HasHealthIssues)
            {
                HasHealthIssues = false;
            }
            else
            {
                HasHealthIssues = true;
            }
        }
        public override string ToString()
        {
            return $"Diver [ Name: {Name}, Oxygen left: {OxygenLevel}, Fish caught: {Catch.Count}, Points earned: {CompetitionPoints} ]";
        }
    }
}
