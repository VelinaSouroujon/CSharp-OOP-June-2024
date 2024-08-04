using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfluencerManagerApp.Models.Influencers
{
    public class BloggerInfluencer : Influencer
    {
        private const double DefaultEngagementRate = 2;
        public BloggerInfluencer(string username, int followers)
            : base(username, followers, DefaultEngagementRate)
        {
        }

        protected override double Factor => 0.2;
    }
}
