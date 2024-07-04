using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilitaryElite
{
    public class Engineer : SpecialisedSoldier, IEngineer
    {
        private List<IRepair> repairs;
        public Engineer(string id, string firstName, string lastName, decimal salary, Corps corps)
            : base(id, firstName, lastName, salary, corps)
        {
            repairs = new List<IRepair>();
            Repairs = repairs.AsReadOnly();
        }

        public IReadOnlyCollection<IRepair> Repairs { get; }
        public void AddRepair(IRepair repair)
        {
            if (repair == null)
            {
                throw new ArgumentNullException(nameof(repair));
            }

            repairs.Add(repair);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine();
            sb.AppendLine("Repairs:");

            foreach (IRepair repair in Repairs)
            {
                sb.AppendLine($"  {repair}");
            }

            return base.ToString() + sb.ToString().TrimEnd();
        }
    }
}
