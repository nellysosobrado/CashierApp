using CashierApp.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashierApp.Application.Services
{
    public static class PriceCalculator
    {
        private static CampaignService _campaignManager;

        public static void Initialize(CampaignService campaignManager)
        {
            _campaignManager = campaignManager ?? throw new ArgumentNullException(nameof(campaignManager));
        }

        public static decimal CalculateTotalPrice(List<(IProducts Product, int Quantity)> cart)
        {
            if (_campaignManager == null)
                throw new InvalidOperationException("PriceCalculator has not been initialized with a CampaignManager.");

            decimal total = 0;

            foreach (var item in cart)
            {
                var product = item.Product;
                var activeCampaigns = _campaignManager.GetActiveCampaigns(product.Campaigns);

                if (activeCampaigns.Any())
                {
                    total += activeCampaigns.First().CampaignPrice.Value * item.Quantity;
                }
                else
                {
                    total += product.Price * item.Quantity;
                }
            }

            return total;
        }
    }
}
