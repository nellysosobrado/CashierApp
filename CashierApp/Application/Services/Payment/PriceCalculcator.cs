using CashierApp.Application.Services.Campaigns;
using CashierApp.Core.Entities;
using CashierApp.Core.Interfaces.StoreProducts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CashierApp.Application.Services.Payment
{
    /// <summary>
    /// Calculates the total price for a shopping cart, considering active campaigns
    /// </summary>
    public class PriceCalculator
    {
        private readonly CampaignService _campaignService;
        public PriceCalculator(CampaignService campaignService)
        {
            if (campaignService == null)
                throw new ArgumentNullException(nameof(campaignService));

            _campaignService = campaignService;
        }
        /// <summary>
        /// Calculates the total price for all items in the cart, applying campaign discounts if available.
        /// </summary>
        /// <param name="cart">A list of products with their quantities.</param>
        /// <returns>The total price of the cart, including campaign discounts.</returns>
        public decimal CalculateTotalPrice(List<(IProducts Product, int Quantity)> cart)
        {
            decimal finalAmount = 0;

            foreach (var item in cart)
            {
                // Check if there is an active campaign for the product
                var campaign = _campaignService.GetCampaignForProduct(item.Product.ProductID);
                // Calculate the price for the quantity of this product
                decimal totalPrice = item.Quantity * item.Product.Price;

                // Apply campaign discount if active
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
