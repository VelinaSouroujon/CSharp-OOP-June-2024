using HighwayToPeak.Models.Contracts;
using HighwayToPeak.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HighwayToPeak.Repositories
{
    public class PeakRepository : IRepository<IPeak>
    {
        private ICollection<IPeak> all;

        public PeakRepository()
        {
            all = new List<IPeak>();
            All = (IReadOnlyCollection<IPeak>)all;
        }
        public IReadOnlyCollection<IPeak> All { get; }

        public void Add(IPeak model)
        {
            all.Add(model);
        }

        public IPeak Get(string name)
        {
            return all.FirstOrDefault(x => x.Name == name);
        }
    }
}
