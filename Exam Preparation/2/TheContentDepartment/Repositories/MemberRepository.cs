using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheContentDepartment.Models.Contracts;
using TheContentDepartment.Repositories.Contracts;

namespace TheContentDepartment.Repositories
{
    public class MemberRepository : IRepository<ITeamMember>
    {
        private ICollection<ITeamMember> models;

        public MemberRepository()
        {
            models = new List<ITeamMember>();
            Models = (IReadOnlyCollection<ITeamMember>)models;
        }
        public IReadOnlyCollection<ITeamMember> Models { get; }

        public void Add(ITeamMember model)
        {
            models.Add(model);
        }

        public ITeamMember TakeOne(string modelName)
        {
            return models.FirstOrDefault(x => x.Name == modelName);
        }
    }
}
