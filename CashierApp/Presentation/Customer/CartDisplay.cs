using CashierApp.Application.Services.Campaigns;
using CashierApp.Application.Services.Payment;
using CashierApp.Core.Entities;
using CashierApp.Application.Services.StoreProduct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using CashierApp.Core.Interfaces.ErrorManagement;
using CashierApp.Core.Interfaces.StoreProducts;

namespace CashierApp.Presentation.Customer
{
    /// <summary>
    /// CartDisplay manages the display of the customer's cart, including products, campaigns, and total price.
    /// </summary>
    public class CartDisplay
    {
        private readonly PriceCalculator _priceCalculator;
        private readonly CampaignService _campaignService;
        private readonly IErrorManager _errorManager;

        public CartDisplay(CampaignService campaignManager, IErrorManager errorManager, PriceCalculator priceCalculator)
        {
            _campaignService = campaignManager;
            _errorManager = errorManager;
            _priceCalculator = priceCalculator;
        }
        public void DisplayCart(List<(IProducts Product, int Quantity)> cart)
        {
            Console.Clear();
            DisplayTitle();
            DisplayCartContent(cart);
            DisplayCommand();
        }
        private void DisplayTitle()
        {
            CenterText("╔═══════════════════════════════════════════════╗");
            CenterText("║                     CART                      ║");
            CenterText("╚═══════════════════════════════════════════════╝");
            CenterText("─────────────────────────────────────────────────");
            CenterText(" Product            │    Total");
            CenterText("─────────────────────────────────────────────────");
        }
        private void DisplayCommand()
        {
            CenterText("─────────────────────────────────────────────────");
            CenterText("[1] Product Catalog    [2] Menu    [PAY] PAY");
            CenterText("─────────────────────────────────────────────────");
            Console.Write("                                                      Command: ");
        }
        /// <summary>
        /// Displays the content of the cart, including products, quantities, prices, and campaigns.
        /// </summary>
        private void DisplayCartContent(List<(IProducts Product, int Quantity)> cart)
        {
            var groupedCart = cart
                .GroupBy(item => item.Product.ProductID)
                .Select(group => (
                    Product: group.First().Product,
                    Quantity: group.Sum(item => item.Quantity)
                ))
                .ToList();

            foreach (var item in groupedCart)
            {
                string productLine = $"ID:{item.Product.ProductID,-5} │ {item.Product.ProductName,-17}({item.Product.PriceType})   {item.Quantity} * {item.Product.Price,8:C} ";
                CenterText(productLine);

                var campaign = _campaignService.GetCampaignForProduct(item.Product.ProductID);
                if (campaign != null && !string.IsNullOrWhiteSpace(campaign.Description))
                {
                    string campaignLine = $"{campaign.Description} -{campaign.CampaignPrice:C}";
                    CenterText(campaignLine);
                }
                CenterText("─────────────────────────────────────────────────");
            }
            var totalAmount = _priceCalculator.CalculateTotalPrice(cart);
            CenterText($"Total: {totalAmount,10:C}");
        }
        /// <summary>
        /// Formats the quantity of a product for display, limiting its length if necessary.
        /// </summary>
        private string FormatQuantity(int quantity)
        {
            string quantityDisplay = quantity.ToString();
            return quantityDisplay.Length > 7 ? quantityDisplay.Substring(0, 6) + "…" : quantityDisplay;
        }
        private void CenterText(string text)
        {
            int windowWidth = Console.WindowWidth;
            int leftPadding = Math.Max((windowWidth - text.Length) / 2, 0);
            Console.SetCursorPosition(leftPadding, Console.CursorTop);
            Console.WriteLine(text);
        }
    }

}
