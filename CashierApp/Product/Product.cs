using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CashierApp.Product.Interfaces;

namespace CashierApp.Product
{
    public class Product : IProducts
    {
       
        public int ProductID { get; set; }
        public required string ProductName { get; set; }
        public decimal Price { get; set; }
        public required string PriceType { get; set; }
        public required string Category { get; set; }


       
        public List<Campaign> Campaigns { get; set; } = new List<Campaign>();
        public string? CampaignDescription { get; set; }
        public decimal? CampaignPrice { get; set; }
        public DateTime? CampaignStartDate { get; set; }
        public DateTime? CampaignEndDate { get; set; }

        
        public bool IsCampaignActive()
        {
            if (CampaignPrice.HasValue && CampaignStartDate.HasValue && CampaignEndDate.HasValue)
            {
                DateTime now = DateTime.Now;
                return now >= CampaignStartDate.Value && now <= CampaignEndDate.Value;
            }
            return false;
        }
    }
}
