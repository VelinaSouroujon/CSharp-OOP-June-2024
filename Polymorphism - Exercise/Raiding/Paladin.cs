﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raiding
{
    public class Paladin : BaseHero
    {
        private const int DefaultPower = 100;
        public Paladin(string name)
            : base(name, DefaultPower)
        {

        }

        public override string CastAbility()
            => HealingCastAbility();
    }
}
