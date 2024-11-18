using CashierApp.Application.Services.Campaigns;
using CashierApp.Application.Services.Payment;
using CashierApp.Core.Entities;
using CashierApp.Application.Services.StoreProduct;

using CashierApp.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CashierApp.Presentation.Customer
{
    public class CartDisplay
    {
        private readonly CampaignService _campaignManager;

        public CartDisplay(CampaignService campaignManager)
        {
            _campaignManager = campaignManager;
        }

        public void DisplayCartUI(List<(IProducts Product, int Quantity)> cart)
        {
            Console.Clear();
            DisplayHeader();
            DisplayCartItems(cart);
            DisplayTotal(PriceCalculator.CalculateTotalPrice(cart));
            DisplayFooter();
            PromptCommand();
        }

        private void DisplayHeader()
        {
            CenterText("╔═══════════════════════════════════════════════╗");
            CenterText("║                     CART                      ║");
            CenterText("╚═══════════════════════════════════════════════╝");
            CenterText("─────────────────────────────────────────────────");
            CenterText(" Product            │    Total");
            CenterText("─────────────────────────────────────────────────");
        }


        private void DisplayCartItems(List<(IProducts Product, int Quantity)> cart)
        {
            foreach (var item in cart)
            {
                string productName = FormatProductName(item.Product.ProductName);
                int quantity = item.Quantity;

                decimal unitPrice = item.Product.Price;
                decimal originalPrice = unitPrice * quantity;
                string priceType = item.Product.PriceType; // Hämta pris-typen (t.ex. kg, piece)

                // Hämta aktiva kampanjer
                var activeCampaigns = _campaignManager.GetActiveCampaigns(item.Product.Campaigns);
                decimal discountedPricePerUnit = activeCampaigns.Any()
                    ? activeCampaigns.First().CampaignPrice.Value
                    : unitPrice;

                decimal totalDiscount = (unitPrice - discountedPricePerUnit) * quantity;
                decimal discountedTotalPrice = discountedPricePerUnit * quantity;

                // Visa produktinformationen i formatet t.ex. 7 * 10 kr (kg)
                string productLine = $"ID:{item.Product.ProductID,-5} │ {productName,-17} │ {quantity} * {discountedPricePerUnit,8:C} ({priceType})";
                CenterText(productLine);

                // Visa rabattinformation om en kampanj är aktiv
                if (activeCampaigns.Any())
                {
                    var campaign = activeCampaigns.First();
                    string campaignLine = $"{campaign.Description}: -{totalDiscount,10:C}";
                    CenterText(campaignLine);
                }

                CenterText("─────────────────────────────────────────────────");
            }
        }



        private void DisplayTotal(decimal totalAmount)
        {
            CenterText($"Total: {totalAmount,10:C}");
        }

        private void DisplayFooter()
        {
            Console.WriteLine("                                   ────────────────────────────────────────────────");
            Console.WriteLine("                                        [1] Products    [2] Menu    [PAY] PAY");
            Console.WriteLine("                                   ────────────────────────────────────────────────");
        }

        private void PromptCommand()
        {
            Console.Write("                                                      Command: ");
        }

        private string FormatProductName(string name)
        {
            return name.Length > 17 ? name.Substring(0, 17) + "…" : name;
        }

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
