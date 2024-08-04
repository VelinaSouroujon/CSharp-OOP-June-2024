using InfluencerManagerApp.Core;
using InfluencerManagerApp.Core.Contracts;
using InfluencerManagerApp.Models.Campaigns;
using InfluencerManagerApp.Models.Contracts;
using InfluencerManagerApp.Models.Influencers;
using InfluencerManagerApp.Repositories;

namespace InfluencerManagerApp
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            IEngine engine = new Engine();
            engine.Run();
        }
    }
}
