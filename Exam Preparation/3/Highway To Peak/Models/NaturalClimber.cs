using HighwayToPeak.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HighwayToPeak.Models
{
    public class NaturalClimber : Climber
    {
        private const int InitialStamina = 6;
        private const int RecoveryRate = 2;
        public NaturalClimber(string name)
            : base(name, InitialStamina)
        {
        }
        public override void Rest(int daysCount)
        {
            Stamina += RecoveryRate * daysCount;
        }
        protected override IDictionary<string, int> PopulatePeakNameAndStaminaDecrease()
        {
            return new Dictionary<string, int>()
            {
                ["Hard"] = 4,
                ["Moderate"] = 2
            };
        }
    }
}
