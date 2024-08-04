using InfluencerManagerApp.Models.Contracts;
using InfluencerManagerApp.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfluencerManagerApp.Repositories
{
    public class InfluencerRepository : IRepository<IInfluencer>
    {
        private ICollection<IInfluencer> models;
        public InfluencerRepository()
        {
            models = new List<IInfluencer>();
            Models = (IReadOnlyCollection<IInfluencer>) models;
        }
        public IReadOnlyCollection<IInfluencer> Models { get; }

        public void AddModel(IInfluencer model)
        {
            models.Add(model);
        }

        public IInfluencer FindByName(string username)
        {
            return models.FirstOrDefault(x => x.Username == username);
        }

        public bool RemoveModel(IInfluencer model)
        {
            return models.Remove(model);
        }
    }
}
