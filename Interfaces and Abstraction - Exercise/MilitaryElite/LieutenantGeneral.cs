using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilitaryElite
{
    public class LieutenantGeneral : Private, ILieutenantGeneral
    {
        private readonly List<IPrivate> privates;
        public LieutenantGeneral(string id, string firstName, string lastName, decimal salary)
            : base(id, firstName, lastName, salary)
        {
            privates = new List<IPrivate>();
            Privates = privates.AsReadOnly();
        }

        public IReadOnlyCollection<IPrivate> Privates { get; }
        public void AddSoldier(IPrivate soldier)
        {
            if(soldier == null)
            {
                throw new ArgumentNullException(nameof(soldier));
            }

            privates.Add(soldier);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine();
            sb.AppendLine("Privates:");

            foreach (IPrivate priv in Privates)
            {
                sb.AppendLine($"  {priv}");
            }

            return base.ToString() + sb.ToString().TrimEnd();
        }
    }
}
