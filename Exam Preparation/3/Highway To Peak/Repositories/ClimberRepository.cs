﻿using HighwayToPeak.Models.Contracts;
using HighwayToPeak.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HighwayToPeak.Repositories
{
    public class ClimberRepository : IRepository<IClimber>
    {
        private ICollection<IClimber> all;
        public ClimberRepository()
        {
            all = new List<IClimber>();
            All = (IReadOnlyCollection<IClimber>)all;
        }
        public IReadOnlyCollection<IClimber> All { get; }

        public void Add(IClimber model)
        {
            all.Add(model);
        }

        public IClimber Get(string name)
        {
            return all.FirstOrDefault(x => x.Name == name);
        }
    }
}
