using HighwayToPeak.Models.Contracts;
using HighwayToPeak.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HighwayToPeak.Models
{
    public abstract class Climber : IClimber
    {
        private const int MinStamina = 0;
        private const int MaxStamina = 10;

        private string name;
        private int stamina;
        private ICollection<string> conquredPeaks;

        private IDictionary<string, int> peakNameAndStaminaDecrease;

        protected Climber(string name, int stamina)
        {
            Name = name;
            Stamina = stamina;

            conquredPeaks = new HashSet<string>();
            ConqueredPeaks = (IReadOnlyCollection<string>)conquredPeaks;

            peakNameAndStaminaDecrease = PopulatePeakNameAndStaminaDecrease();
        }

        public string Name
        {
            get => name;

            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.ClimberNameNullOrWhiteSpace);
                }
                name = value;
            }
        }

        public int Stamina
        {
            get => stamina;

            protected set
            {
                if (value > MaxStamina)
                {
                    stamina = MaxStamina;
                }
                else if (value < MinStamina)
                {
                    stamina = MinStamina;
                }
                else
                {
                    stamina = value;
                }
            }
        }

        public IReadOnlyCollection<string> ConqueredPeaks { get; }

        public void Climb(IPeak peak)
        {
            if (!peakNameAndStaminaDecrease.TryGetValue(peak.DifficultyLevel, out int staminaDecrease))
            {
                return;
            }
            conquredPeaks.Add(peak.Name);
            Stamina -= staminaDecrease;
        }

        public abstract void Rest(int daysCount);

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"{GetType().Name} - Name: {Name}, Stamina: {Stamina}");
            sb.Append("Peaks conquered: ");

            string str = ConqueredPeaks.Count == 0 ? "no peaks conquered" : ConqueredPeaks.Count.ToString();

            sb.AppendLine(str);

            return sb.ToString().TrimEnd();
        }

        protected virtual IDictionary<string, int> PopulatePeakNameAndStaminaDecrease()
        {
            return new Dictionary<string, int>()
            {
                ["Extreme"] = 6,
                ["Hard"] = 4,
                ["Moderate"] = 2
            };
        }
    }
}
