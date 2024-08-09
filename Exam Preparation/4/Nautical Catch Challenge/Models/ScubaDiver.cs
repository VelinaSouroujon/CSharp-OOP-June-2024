using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NauticalCatchChallenge.Models
{
    public class ScubaDiver : Diver
    {
        private const int InitialOxygenLevel = 540;
        public ScubaDiver(string name)
            : base(name, InitialOxygenLevel)
        {

        }

        protected override double DecreaseOxygenRate => 0.3;

        public override void RenewOxy()
        {
            OxygenLevel = InitialOxygenLevel;
        }
    }
}
