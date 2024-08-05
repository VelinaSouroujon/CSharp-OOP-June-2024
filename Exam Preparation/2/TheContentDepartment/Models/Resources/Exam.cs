using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheContentDepartment.Models.Resources
{
    public class Exam : Resource
    {
        private const int DefaultPriority = 1;
        public Exam(string name, string creator)
            : base(name, creator, DefaultPriority)
        {
        }
    }
}
