using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashierApp
{
    public class CampaignManager : ICampaignManager
    {
        public bool IsCampaignActive(Campaign campaign)
        {
            if (campaign.CampaignPrice.HasValue && campaign.StartDate.HasValue && campaign.EndDate.HasValue)
            {
                DateTime now = DateTime.Now;
                return now >= campaign.StartDate.Value && now <= campaign.EndDate.Value;
            }
            return false;
        }

        public List<Campaign> GetActiveCampaigns(List<Campaign> campaigns)
        {
            return campaigns.Where(IsCampaignActive).ToList();
        }
    }
}
