using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashierApp
{
    public interface ICampaignManager
    {
        bool IsCampaignActive(Campaign campaign);
        List<Campaign> GetActiveCampaigns(List<Campaign> campaigns);
    }
}
