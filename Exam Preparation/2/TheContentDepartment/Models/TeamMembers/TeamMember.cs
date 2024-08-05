using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheContentDepartment.Models.Contracts;
using TheContentDepartment.Utilities.Messages;

namespace TheContentDepartment.Models.TeamMembers
{
    public abstract class TeamMember : ITeamMember
    {
        private string name;
        private ICollection<string> inProgress;

        protected TeamMember(string name, string path)
        {
            Name = name;
            Path = path;

            inProgress = new List<string>();
            InProgress = (IReadOnlyCollection<string>)inProgress;
        }

        public string Name
        {
            get => name;

            private set
            {
                if(string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.NameNullOrWhiteSpace);
                }
                name = value;
            }
        }

        public abstract string Path { get; protected set; }

        public IReadOnlyCollection<string> InProgress { get; }

        public void FinishTask(string resourceName)
        {
            inProgress.Remove(resourceName);
        }

        public void WorkOnTask(string resourceName)
        {
            inProgress.Add(resourceName);
        }
    }
}
