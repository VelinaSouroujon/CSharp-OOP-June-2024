using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheContentDepartment.Utilities.Messages;

namespace TheContentDepartment.Models.TeamMembers
{
    public class ContentMember : TeamMember
    {
        private static HashSet<string> allowedPaths = new HashSet<string>()
        {
            "CSharp",
            "JavaScript",
            "Python",
            "Java"
        };

        private string path;
        public ContentMember(string name, string path)
            : base(name, path)
        {
        }

        public override string Path
        {
            get => path;

            protected set
            {
                if(!allowedPaths.Contains(value))
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.PathIncorrect, value));
                }
                path = value;
            }
        }
        public override string ToString()
        {
            return $"{Name} - {Path} path. Currently working on {InProgress.Count} tasks.";
        }
    }
}
