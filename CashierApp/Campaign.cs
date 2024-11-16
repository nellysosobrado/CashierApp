using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashierApp
{
    public class Campaign
    {
        public string Description { get; set; } = string.Empty; 
        public decimal? CampaignPrice { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; } 
    }
}
