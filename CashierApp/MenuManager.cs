using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashierApp
{
    public class MenuManager
    {
        public string ShowMainMenu()
        {
            Console.Clear();
            Console.WriteLine("=== Main Menu ===");
            Console.WriteLine("1. New Customer");
            Console.WriteLine("2. Admin Settings");
            Console.WriteLine("3. Exit");


            Console.WriteLine("Type 'exit' to quit.");
            Console.Write("Enter your choice: ");
            return Console.ReadLine()?.Trim();
        }
    }
}
