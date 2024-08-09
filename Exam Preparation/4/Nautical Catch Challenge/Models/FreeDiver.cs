using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NauticalCatchChallenge.Models
{
    public class FreeDiver : Diver
    {
        private const int InitialOxygenLevel = 120;
        public FreeDiver(string name)
            : base(name, InitialOxygenLevel)
        {

        }

        protected override double DecreaseOxygenRate => 0.6;

        public override void RenewOxy()
        {
            OxygenLevel = InitialOxygenLevel;
        }
    }
}
