using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfluencerManagerApp.Models.Influencers
{
    public class BusinessInfluencer : Influencer
    {
        private const double DefaultEngagementRate = 3;
        public BusinessInfluencer(string username, int followers)
            : base(username, followers, DefaultEngagementRate)
        {
        }

        protected override double Factor => 0.15;
    }
}
