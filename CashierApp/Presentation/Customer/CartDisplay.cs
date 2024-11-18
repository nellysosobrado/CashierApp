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
            DisplayTitle();
            DisplayCartItems(cart);
            DisplayTotal(PriceCalculator.CalculateTotalPrice(cart));
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


        private void DisplayCartItems(List<(IProducts Product, int Quantity)> cart)
        {
            foreach (var item in cart)
            {
                string productLine = $"ID:{item.Product.ProductID,-5} │ {item.Product.ProductName,-17}   {item.Quantity} * {item.Product.Price,8:C}";
                CenterText(productLine);
                CenterText("─────────────────────────────────────────────────");
            }
        }
        private void DisplayTotal(decimal totalAmount)
        {
            CenterText($"Total: {totalAmount,10:C}");
        }
        private void DisplayCommand()
        {
            Console.WriteLine("                                   ────────────────────────────────────────────────");
            Console.WriteLine("                                        [1] Products    [2] Menu    [PAY] PAY");
            Console.WriteLine("                                   ────────────────────────────────────────────────");
            Console.Write("                                                      Command: ");
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
