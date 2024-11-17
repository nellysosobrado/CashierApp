using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CashierApp.Core.Entities;

namespace CashierApp.Core.Interfaces
{
    public interface ICampaignManager
    {
        void AddCampaign();
        void RemoveCampaign();
        bool IsCampaignActive(Campaign campaign);
        List<Campaign> GetActiveCampaigns(List<Campaign> campaigns);

    }
}
