using CashierApp.Payment;
using CashierApp.Product.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashierApp.Customer
{
    public class CartDisplay
    {
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
            CenterText("║                     CASHIER                   ║");
            CenterText("╚═══════════════════════════════════════════════╝");
            Console.WriteLine("                                                      Current Cart");
            Console.WriteLine("                                   ────────────────────────────────────────────────");
            Console.WriteLine("                                    ID    │ Product         │   Qty     │    Total");
            Console.WriteLine("                                   ────────────────────────────────────────────────");
        }

        private void DisplayCartItems(List<(IProducts Product, int Quantity)> cart)
        {
            foreach (var item in cart)
            {
                string productName = FormatProductName(item.Product.ProductName);
                string quantityDisplay = FormatQuantity(item.Quantity);

                decimal originalPrice = item.Product.Price * item.Quantity;
                decimal discountedPrice = item.Product.IsCampaignActive() ? item.Product.CampaignPrice.Value * item.Quantity : originalPrice;
                decimal discount = originalPrice - discountedPrice;

                
                Console.WriteLine($"                                    {item.Product.ProductID,-5} │ {productName,-17} │ {quantityDisplay,7} │ {originalPrice,10:C}");

                // Checks campaign
                if (item.Product.IsCampaignActive())
                {
                    string campaignDescription = item.Product.CampaignDescription ?? "Campaign";
                    if (!string.IsNullOrWhiteSpace(item.Product.CampaignDescription))
                    {
                        // Visa kampanjbeskrivning om den finns
                        Console.WriteLine($"{"",-54}{item.Product.CampaignDescription}:       -{discount,10:C}");
                    }
                    else
                    {
                        // Visa endast rabatten utan text
                        Console.WriteLine($"{"",-73}-{discount,10:C}");
                    }
                    // Console.WriteLine($"                                              Campaign Price: {discountedPrice,10:C}");
                    //Console.WriteLine($"                                                       {campaignDescription}:       -{discount,10:C}");
                }
            }
        }

        private void DisplayTotal(decimal totalAmount)
        {
            Console.WriteLine("                                   ────────────────────────────────────────────────");
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
