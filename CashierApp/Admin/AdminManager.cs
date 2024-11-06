using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CashierApp.Admin
{
    public class AdminManager
    {

        public void HandleAdmin()
        {
            Console.WriteLine("=== Admin Menu ===");
            Console.WriteLine("1. Add product");
            Console.WriteLine("2. Edit product");
            Console.WriteLine("Enter your choice: ");
            var choice = Console.ReadLine()?.Trim();

            if (choice == "1")
            {
                Console.WriteLine("Adding product...");
            }
            else if (choice == "2")
            {
                Console.WriteLine("Editing product...");
            }
        }
    }
}
