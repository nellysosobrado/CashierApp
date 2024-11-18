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
        private static CampaignService _campaignManager;

        public static void SetCampaignManager(CampaignService campaignManager) =>
            _campaignManager = campaignManager ?? throw new ArgumentNullException(nameof(campaignManager));

        public static decimal CalculateTotalPrice(List<(IProducts Product, int Quantity)> cart)
        {
            return cart.Sum(item =>
            {
                return item.Product.Price * item.Quantity; 
            });
        }

    }
}
