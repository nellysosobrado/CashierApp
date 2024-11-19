using CashierApp.Application.Services.Campaigns;
using CashierApp.Core.Entities;
using CashierApp.Core.Interfaces.StoreProducts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CashierApp.Application.Services.Payment
{
    public class PriceCalculator
    {
        private readonly CampaignService _campaignService;

        public PriceCalculator(CampaignService campaignService)
        {
            _campaignService = campaignService ?? throw new ArgumentNullException(nameof(campaignService));
        }

        public decimal CalculateTotalPrice(List<(IProducts Product, int Quantity)> cart)
        {
            if (_campaignService == null)
            {
                throw new InvalidOperationException("CampaignService has not been set.");
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
