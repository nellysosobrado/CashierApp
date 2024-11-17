using CashierApp.Product.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashierApp.Payment
{
    public static class PriceCalculator
    {
        private static CampaignManager _campaignManager = new CampaignManager();

        public static decimal CalculateTotalPrice(List<(IProducts Product, int Quantity)> cart)
        {
            decimal total = 0;

            foreach (var item in cart)
            {
                var product = item.Product;

                // Hämta aktiva kampanjer
                var activeCampaigns = _campaignManager.GetActiveCampaigns(product.Campaigns);

                if (activeCampaigns.Any())
                {
                    // Använd det första kampanjpriset
                    total += activeCampaigns.First().CampaignPrice.Value * item.Quantity;
                }
                else
                {
                    // Använd standardpris om ingen kampanj är aktiv
                    total += product.Price * item.Quantity;
                }
            }

            return total;
        }
    }
}
