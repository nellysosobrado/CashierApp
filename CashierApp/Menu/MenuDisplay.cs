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
            CenterText("Use arrow keys and 'Enter' to select a option");
            CenterText("╔══════════════════════════════════════╗");

            for (int i = 0; i <options.Length; i++)
            {
                string line;
                if( i == selectedIndex)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    //Console.WriteLine($"-> {options[i]}");
                    line = $"║ -> {options[i].PadRight(30)}    ║";
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Gray;
                    //Console.WriteLine($" {options[i]}");
                    line = $"║    {options[i].PadRight(30)}    ║";
                }
                CenterText(line);

            }
            CenterText("╚══════════════════════════════════════╝");
            Console.ResetColor();
        }
        private void CenterText(string text)
        {
            int windowWidth = Console.WindowWidth;
            int textWidth = text.Length;
            int padding = (windowWidth - textWidth) / 2;
            Console.WriteLine(new string(' ', padding) + text);
        }
    }
}
