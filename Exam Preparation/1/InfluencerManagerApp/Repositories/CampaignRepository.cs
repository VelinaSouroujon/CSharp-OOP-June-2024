using InfluencerManagerApp.Models.Contracts;
using InfluencerManagerApp.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfluencerManagerApp.Repositories
{
    public class CampaignRepository : IRepository<ICampaign>
    {
        private ICollection<ICampaign> models;
        public CampaignRepository()
        {
            models = new List<ICampaign>();
            Models = (IReadOnlyCollection<ICampaign>) models;
        }
        public IReadOnlyCollection<ICampaign> Models { get; }

        public void AddModel(ICampaign model)
        {
            models.Add(model);
        }

        public ICampaign FindByName(string brand)
        {
            return models.FirstOrDefault(x => x.Brand == brand);
        }

        public bool RemoveModel(ICampaign model)
        {
            return models.Remove(model);
        }
    }
}
