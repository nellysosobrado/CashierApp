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
        public static decimal CalculateTotalPrice(List<(IProducts Product, int Quantity)> cart)
        {
            decimal total = 0;
            var currentDate = DateTime.Now;

            foreach (var item in cart)
            {
                var product = item.Product;

                // Använd kampanjpris om det är giltigt
                if (product.CampaignPrice.HasValue &&
                    product.CampaignStartDate <= currentDate &&
                    product.CampaignEndDate >= currentDate)
                {
                    total += product.CampaignPrice.Value * item.Quantity;
                }
                else
                {
                    // Annars använd vanligt pris
                    total += product.Price * item.Quantity;
                }
            }

            return total;
        }
    }
}
