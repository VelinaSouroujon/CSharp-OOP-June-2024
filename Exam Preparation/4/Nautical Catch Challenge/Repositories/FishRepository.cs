using NauticalCatchChallenge.Models.Contracts;
using NauticalCatchChallenge.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NauticalCatchChallenge.Repositories
{
    public class FishRepository : IRepository<IFish>
    {
        private readonly ICollection<IFish> models;

        public FishRepository()
        {
            models = new List<IFish>();
            Models = (IReadOnlyCollection<IFish>)models;
        }
        public IReadOnlyCollection<IFish> Models { get; }

        public void AddModel(IFish model)
        {
            models.Add(model);
        }

        public IFish GetModel(string name)
        {
            return models.FirstOrDefault(x => x.Name == name);
        }
    }
}
