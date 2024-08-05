using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheContentDepartment.Utilities.Messages;

namespace TheContentDepartment.Models.TeamMembers
{
    public class TeamLead : TeamMember
    {
        private const string DefaultPath = "Master";

        private string path;
        public TeamLead(string name, string path)
            : base(name, path)
        {
        }

        public override string Path
        {
            get => path;

            protected set
            {
                if(value != DefaultPath)
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.PathIncorrect, value));
                }
                path = value;
            }
        }
        public override string ToString()
        {
            return $"{Name} ({GetType().Name}) - Currently working on {InProgress.Count} tasks.";
        }
    }
}
