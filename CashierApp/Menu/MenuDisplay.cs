using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashierApp.Menu
{
    public class MenuDisplay
    {
        public void ShowMenuOptions(string[] options, int selectedIndex)
        {
            Console.Clear();
            Console.WriteLine("Use arrow keys and 'Enter' to select a option");
            Console.WriteLine("\n╔══════════════════════════════════════╗");

            for (int i = 0; i <options.Length; i++)
            {
                if( i == selectedIndex)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"-> {options[i]}");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine($" {options[i]}");
                }
                
            }
            Console.WriteLine("╚══════════════════════════════════════╝");
            Console.ResetColor();
        }
    }
}
