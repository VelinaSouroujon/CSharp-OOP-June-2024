using InfluencerManagerApp.Core.Contracts;
using InfluencerManagerApp.Models.Campaigns;
using InfluencerManagerApp.Models.Contracts;
using InfluencerManagerApp.Models.Influencers;
using InfluencerManagerApp.Repositories;
using InfluencerManagerApp.Repositories.Contracts;
using InfluencerManagerApp.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfluencerManagerApp.Core
{
    public class Controller : IController
    {
        private readonly IRepository<IInfluencer> influencers;
        private readonly IRepository<ICampaign> campaigns;

        public Controller()
        {
            influencers = new InfluencerRepository();
            campaigns = new CampaignRepository();
        }
        public string ApplicationReport()
        {
            StringBuilder sb = new StringBuilder();

            foreach(IInfluencer influencer in influencers.Models
                .OrderByDescending(x => x.Income)
                .ThenByDescending(x => x.Followers))
            {
                sb.AppendLine(influencer.ToString());

                if(influencer.Participations.Count == 0)
                {
                    continue;
                }

                sb.AppendLine("Active Campaigns:");
                foreach(string brand in influencer.Participations.OrderBy(x => x))
                {
                    ICampaign campaign = campaigns.Models.First(x => x.Brand == brand);
                    sb.AppendLine($"--{campaign}");
                }
            }

            return sb.ToString().TrimEnd();
        }

        public string AttractInfluencer(string brand, string username)
        {
            IInfluencer influencer = influencers.Models.FirstOrDefault(x => x.Username == username);
            ICampaign campaign = campaigns.Models.FirstOrDefault(x => x.Brand == brand);

            if (influencer == null)
            {
                return string.Format(OutputMessages.InfluencerNotFound, nameof(InfluencerRepository), username);
            }
            if(campaign == null)
            {
                return string.Format(OutputMessages.CampaignNotFound, brand);
            }
            if(campaign.Contributors.Contains(username))
            {
                return string.Format(OutputMessages.InfluencerAlreadyEngaged, username, brand);
            }

            bool isEligible = false;
            string campaignType = campaign.GetType().Name;
            string influencerType = influencer.GetType().Name;

            if(campaignType == nameof(ProductCampaign))
            {
                isEligible = influencerType == nameof(BusinessInfluencer)
                    || influencerType == nameof(FashionInfluencer);
            }
            else if(campaignType == nameof(ServiceCampaign))
            {
                isEligible = influencerType == nameof(BusinessInfluencer)
                    || influencerType == nameof(BloggerInfluencer);
            }
            if(!isEligible)
            {
                return string.Format(OutputMessages.InfluencerNotEligibleForCampaign, username, brand);
            }
            if(campaign.Budget < influencer.CalculateCampaignPrice())
            {
                return string.Format(OutputMessages.UnsufficientBudget, brand, username);
            }

            influencer.EnrollCampaign(brand);
            campaign.Engage(influencer);
            influencer.EarnFee(influencer.CalculateCampaignPrice());

            return string.Format(OutputMessages.InfluencerAttractedSuccessfully, username, brand);
        }

        public string BeginCampaign(string typeName, string brand)
        {
            ICampaign campaign;
            switch(typeName)
            {
                case nameof(ProductCampaign):
                    campaign = new ProductCampaign(brand);
                    break;

                case nameof(ServiceCampaign):
                    campaign = new ServiceCampaign(brand);
                    break;

                default:
                    return string.Format(OutputMessages.CampaignTypeIsNotValid, typeName);
            }

            if(campaigns.Models.Any(x => x.Brand == brand))
            {
                return string.Format(OutputMessages.CampaignDuplicated, brand);
            }

            campaigns.AddModel(campaign);

            return string.Format(OutputMessages.CampaignStartedSuccessfully, brand, typeName);
        }

        public string CloseCampaign(string brand)
        {
            ICampaign campaign = campaigns.Models.FirstOrDefault(x => x.Brand == brand);
            if(campaign == null)
            {
                return OutputMessages.InvalidCampaignToClose;
            }

            int minBudget = 10000;
            if(campaign.Budget <= minBudget)
            {
                return string.Format(OutputMessages.CampaignCannotBeClosed, brand);
            }

            int bonusForInfluencers = 2000;
            foreach(string name in campaign.Contributors)
            {
                IInfluencer influencer = influencers.Models.First(x => x.Username == name);
                influencer.EarnFee(bonusForInfluencers);
                influencer.EndParticipation(brand);
            }

            campaigns.RemoveModel(campaign);

            return string.Format(OutputMessages.CampaignClosedSuccessfully, brand);
        }

        public string ConcludeAppContract(string username)
        {
            IInfluencer influencer = influencers.Models.FirstOrDefault(x => x.Username == username);
            if(influencer == null)
            {
                return string.Format(OutputMessages.InfluencerNotSigned, username);
            }
            if(influencer.Participations.Count > 0)
            {
                return string.Format(OutputMessages.InfluencerHasActiveParticipations, username);
            }

            influencers.RemoveModel(influencer);

            return string.Format(OutputMessages.ContractConcludedSuccessfully, username);
        }

        public string FundCampaign(string brand, double amount)
        {
            ICampaign campaign = campaigns.Models.FirstOrDefault(x => x.Brand == brand);
            if(campaign == null)
            {
                return OutputMessages.InvalidCampaignToFund;
            }
            if(amount <= 0)
            {
                return OutputMessages.NotPositiveFundingAmount;
            }

            campaign.Gain(amount);

            return string.Format(OutputMessages.CampaignFundedSuccessfully, brand, amount);
        }

        public string RegisterInfluencer(string typeName, string username, int followers)
        {
            IInfluencer influencer;
            switch (typeName)
            {
                case nameof(BusinessInfluencer):
                    influencer = new BusinessInfluencer(username, followers);
                    break;

                case nameof(FashionInfluencer):
                    influencer = new FashionInfluencer(username, followers);
                    break;

                case nameof(BloggerInfluencer):
                    influencer = new BloggerInfluencer(username, followers);
                    break;

                default:
                    return string.Format(OutputMessages.InfluencerInvalidType, typeName);
            }

            if(influencers.Models.Any(x => x.Username == username))
            {
                return string.Format(OutputMessages.UsernameIsRegistered, username, nameof(InfluencerRepository));
            }

            influencers.AddModel(influencer);

            return string.Format(OutputMessages.InfluencerRegisteredSuccessfully, username);
        }
    }
}
