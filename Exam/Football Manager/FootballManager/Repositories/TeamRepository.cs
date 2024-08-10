using FootballManager.Models.Contracts;
using FootballManager.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballManager.Repositories
{
    public class TeamRepository : IRepository<ITeam>
    {
        private ICollection<ITeam> models;
        private const int MaxCapacity = 10;

        public TeamRepository()
        {
            models = new List<ITeam>();
            Models = (IReadOnlyCollection<ITeam>)models;
        }
        public IReadOnlyCollection<ITeam> Models { get; }

        public int Capacity => models.Count;

        public void Add(ITeam model)
        {
            if (Capacity == MaxCapacity)
            {
                return;
            }

            models.Add(model);
        }

        public bool Exists(string name)
        {
            return models.Any(x => x.Name == name);
        }

        public ITeam Get(string name)
        {
            return models.FirstOrDefault(x => x.Name == name);
        }

        public bool Remove(string name)
        {
            ITeam teamToRemove = models.FirstOrDefault(x => x.Name == name);
            if(teamToRemove is null)
            {
                return false;
            }

            models.Remove(teamToRemove);
            return true;
        }
    }
}
