using NauticalCatchChallenge.Models.Contracts;
using NauticalCatchChallenge.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NauticalCatchChallenge.Repositories
{
    public class DiverRepository : IRepository<IDiver>
    {
        private readonly ICollection<IDiver> models;

        public DiverRepository()
        {
            models = new List<IDiver>();
            Models = (IReadOnlyCollection<IDiver>)models;
        }
        public IReadOnlyCollection<IDiver> Models { get; }

        public void AddModel(IDiver model)
        {
            models.Add(model);
        }

        public IDiver GetModel(string name)
        {
            return models.FirstOrDefault(x => x.Name == name);
        }
    }
}
