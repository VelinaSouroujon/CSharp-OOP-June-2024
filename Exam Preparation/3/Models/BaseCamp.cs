using HighwayToPeak.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HighwayToPeak.Models
{
    public class BaseCamp : IBaseCamp
    {
        private ICollection<string> residents;

        public BaseCamp()
        {
            residents = new SortedSet<string>();
            Residents = (IReadOnlyCollection<string>)residents;
        }
        public IReadOnlyCollection<string> Residents { get; }

        public void ArriveAtCamp(string climberName)
        {
            residents.Add(climberName);
        }

        public void LeaveCamp(string climberName)
        {
            residents.Remove(climberName);
        }
    }
}
