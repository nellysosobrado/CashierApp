﻿using CashierApp.Product.Interfaces;
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
            Console.WriteLine("\n                                   ╔═══════════════════════════════════════════════╗");
            Console.WriteLine("                                   ║              KYH CHECKOUT SYSTEM              ║");
            Console.WriteLine("                                   ╚═══════════════════════════════════════════════╝");
            Console.WriteLine("                                                      Current Cart");
            Console.WriteLine("                                   ────────────────────────────────────────────────");
            Console.WriteLine("                                    ID    │ Product Name        │   Qty   │      Total");
            Console.WriteLine("                                   ────────────────────────────────────────────────");

            decimal grandTotal = 0;
            foreach (var item in cart)
            {
                decimal total = item.Product.Price * item.Quantity;
                // Trunkera produktnamn om det är längre än 17 tecken
                string productName = item.Product.Name.Length > 17 ? item.Product.Name.Substring(0, 17) + "…" : item.Product.Name;

                // Begränsa kvantiteten till max 7 tecken
                string quantityDisplay = item.Quantity.ToString();
                if (quantityDisplay.Length > 7)
                {
                    quantityDisplay = quantityDisplay.Substring(0, 6) + "…"; // Trunkera och lägg till "…"
                }

                Console.WriteLine($"                                    {item.Product.ProductID,-5} │ {productName,-17} │ {quantityDisplay,7} │ {total}");
                grandTotal += total;
            }

            
            //if (grandTotal > 50)
            //{
            //    string campaignText = "  Campaign Price                -2.00 KR (Saved)";
            //    Console.WriteLine($"{"",35}{campaignText,50}"); // 35 mellanslag för att centrera texten i din layout
            //}

            Console.WriteLine("                                   ────────────────────────────────────────────────");
            Console.WriteLine($"                                                 Total: {grandTotal,10:C}");
            Console.WriteLine("                                   ────────────────────────────────────────────────");
            Console.WriteLine("                                        [1] Products    [PAY] Pay    [3] Menu");
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
