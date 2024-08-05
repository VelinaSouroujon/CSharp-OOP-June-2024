using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheContentDepartment.Models.Contracts;
using TheContentDepartment.Repositories.Contracts;

namespace TheContentDepartment.Repositories
{
    public class ResourceRepository : IRepository<IResource>
    {
        private ICollection<IResource> models;

        public ResourceRepository()
        {
            models = new List<IResource>();
            Models = (IReadOnlyCollection<IResource>)models;
        }
        public IReadOnlyCollection<IResource> Models { get; }

        public void Add(IResource model)
        {
            models.Add(model);
        }

        public IResource TakeOne(string modelName)
        {
            return models.FirstOrDefault(x => x.Name == modelName);
        }
    }
}
