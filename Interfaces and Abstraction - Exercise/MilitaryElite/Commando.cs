using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilitaryElite
{
    public class Commando : SpecialisedSoldier, ICommando
    {
        private List<IMission> missions;
        public Commando(string id, string firstName, string lastName, decimal salary, Corps corps)
            : base(id, firstName, lastName, salary, corps)
        {
            missions = new List<IMission>();
            Missions = missions.AsReadOnly();
        }

        public IReadOnlyCollection<IMission> Missions { get; }

        public void AddMission(IMission mission)
        {
            if (mission == null)
            {
                throw new ArgumentNullException(nameof(mission));
            }

            missions.Add(mission);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine();
            sb.AppendLine("Missions:");

            foreach (IMission mission in missions)
            {
                sb.AppendLine($"  {mission}");
            }

            return base.ToString() + sb.ToString().TrimEnd();
        }
    }
}
