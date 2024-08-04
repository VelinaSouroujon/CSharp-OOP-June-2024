using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfluencerManagerApp.Models.Influencers
{
    public class FashionInfluencer : Influencer
    {
        private const double DefaultEngagementRate = 4;
        public FashionInfluencer(string username, int followers)
            : base(username, followers, DefaultEngagementRate)
        {
        }

        protected override double Factor => 0.1;
    }
}
