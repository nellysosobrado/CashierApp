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
            
            //Console.Clear(); 
            Console.WriteLine();
            Console.WriteLine("                           ╔═══════════════════════════════════════════════╗");
            Console.WriteLine("                           ║              KYH CHECKOUT SYSTEM              ║");
            Console.WriteLine("                           ╚═══════════════════════════════════════════════╝");
            Console.WriteLine("                                       Current Cart");
            Console.WriteLine("                           ────────────────────────────────────────────────");
            Console.WriteLine("                           ID   │ Product Name        │ Qty │ Total");
            Console.WriteLine("                           ────────────────────────────────────────────────");

            decimal grandTotal = 0;
            foreach (var item in cart)
            {
                decimal total = item.Product.Price * item.Quantity;
                Console.WriteLine($"                           {item.Product.ProductID,-4} │ {item.Product.Name,-17} │ {item.Quantity,3} │ {total,6:C}");
                grandTotal += total;
            }

            
            if (grandTotal > 50) 
            {
                Console.WriteLine($"                           │  Campaign Price     │     │  -2.00 KR (Saved)");
            }

            Console.WriteLine("                           ────────────────────────────────────────────────");
            Console.WriteLine($"                                         Total: {grandTotal:C}");
            Console.WriteLine("                           ────────────────────────────────────────────────");
            Console.WriteLine("                                 [1] Products    [PAY] Pay    [3] Menu");
            Console.WriteLine("                           ────────────────────────────────────────────────");
            Console.Write("                           Command: ");

            
        }
    }
}
