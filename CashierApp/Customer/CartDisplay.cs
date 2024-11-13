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
            Console.WriteLine("\n                                   ╔═══════════════════════════════════════════════╗");
            Console.WriteLine("                                   ║                     CASHIER                   ║");
            Console.WriteLine("                                   ╚═══════════════════════════════════════════════╝");
            Console.WriteLine("                                                      Current Cart");
            Console.WriteLine("                                   ────────────────────────────────────────────────");
            Console.WriteLine("                                    ID    │ Product         │   Qty     │    Total");
            Console.WriteLine("                                   ────────────────────────────────────────────────");

            foreach (var item in cart)
            {
                decimal total = item.Product.Price * item.Quantity;

                string productName = item.Product.ProductName.Length > 17 ? item.Product.ProductName.Substring(0, 17) + "…" : item.Product.ProductName;

                string quantityDisplay = item.Quantity.ToString();
                if (quantityDisplay.Length > 7)
                {
                    quantityDisplay = quantityDisplay.Substring(0, 6) + "…";
                }

                Console.WriteLine($"                                    {item.Product.ProductID,-5} │ {productName,-17} │ {quantityDisplay,7} │ {total}");
                
            }
            decimal TotalAmount = PriceCalculator.CalculateTotalPrice(cart);
            
            //if (TotalAmount > 50)
            //{
            //    string campaignText = "  Campaign Price                -2.00 KR (Saved)";
            //    Console.WriteLine($"{"",35}{campaignText,50}"); // 35 mellanslag för att centrera texten i din layout
            //}

            Console.WriteLine("                                   ────────────────────────────────────────────────");
            Console.WriteLine($"                                                 Total: {TotalAmount,10:C}");
            Console.WriteLine("                                   ────────────────────────────────────────────────");
            Console.WriteLine("                                        [1] Products    [2] Menu    [PAY] PAY");
            Console.WriteLine("                                   ────────────────────────────────────────────────");
            Console.Write("                                                      Command: ");
        }
        private void CenterText(string text)
        {
            int windowWidth = Console.WindowWidth;
            int leftPadding = (windowWidth - text.Length) / 2;
            if (leftPadding < 0) leftPadding = 0; 

            Console.SetCursorPosition(leftPadding, Console.CursorTop);
            Console.WriteLine(text);
        }
    }
}
