using CashierApp.Application.Services.Campaigns;
using CashierApp.Core.Entities;
using CashierApp.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CashierApp.Application.Services.Payment
{
    public static class PriceCalculator
    {
        private static CampaignService _campaignService;

        public static void SetCampaignManager(CampaignService campaignManager) =>
            _campaignService = campaignManager ?? throw new ArgumentNullException(nameof(campaignManager));

        public static decimal CalculateTotalPrice(List<(IProducts Product, int Quantity)> cart)
        {
            if (_campaignService == null)
            {
                throw new InvalidOperationException("CampaignService has not been set. Please call SetCampaignManager.");
            }

            decimal finalAmount = 0;

            foreach (var item in cart)
            {
                var campaign = _campaignService.GetCampaignForProduct(item.Product.ProductID);

                decimal totalPrice = item.Quantity * item.Product.Price;

                if (campaign != null && _campaignService.IsCampaignActive(campaign))
                {
                    finalAmount += totalPrice - (campaign.CampaignPrice ?? 0); 
                }
                else
                {
                    finalAmount += totalPrice; 
                }
            }

            return finalAmount;
        }
    }
}
