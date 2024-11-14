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

                // Kontrollera om kampanjen är aktiv och använd kampanjpriset om det finns
                decimal price = item.Product.IsCampaignActive() ? item.Product.CampaignPrice.Value : item.Product.Price;
                decimal total = price * item.Quantity;

                Console.WriteLine($"                                    {item.Product.ProductID,-5} │ {productName,-17} │ {quantityDisplay,7} │ {total:C}");
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
