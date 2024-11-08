using CashierApp.Product.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashierApp.Customer
{
    public class NewCustomer
    {
        public void ShowCart(List<(IProducts Product, int Quantity)> cart)
        {
            Console.Clear();
            Console.WriteLine();
            CenterText("╔═══════════════════════════════════════════════╗");
            CenterText("║              KYH CHECKOUT SYSTEM              ║");
            CenterText("╚═══════════════════════════════════════════════╝");
            CenterText("Current Cart");
            CenterText("───────────────────────────────────────────────");
            CenterText("ID   │ Product Name        │ Qty │ Total");
            CenterText("───────────────────────────────────────────────");

            decimal grandTotal = 0;
            foreach (var item in cart)
            {
                decimal total = item.Product.Price * item.Quantity;
                CenterText($"{item.Product.ProductID,-4}   │   {item.Product.Name,-17} │ {item.Quantity,3} │ {total,6:C}");
                grandTotal += total;
            }

            if (grandTotal > 50)
            {
                CenterText("│  Campaign Price     │     │  -2.00 KR (Saved)");
            }

            CenterText("───────────────────────────────────────────────");
            CenterText($"Total: {grandTotal:C}");
            CenterText("───────────────────────────────────────────────");
            CenterText("[1] Products    [PAY] Pay    [3] Menu");
            CenterText("───────────────────────────────────────────────");

            int windowWidth = Console.WindowWidth;
            string commandText = "Command: ";
            int leftPadding = (windowWidth - commandText.Length) / 2;
            Console.SetCursorPosition(leftPadding, Console.CursorTop);
            Console.Write(commandText);


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
