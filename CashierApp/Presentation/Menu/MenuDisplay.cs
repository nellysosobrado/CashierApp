using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashierApp.Presentation.Menu
{
    /// <summary>
    /// Handles displaying centered and visually formatted menu options 
    /// for the cashier system, including navigation instructions '> <'
    /// </summary>
    public class MenuDisplay
    {
        public void ShowMenuOptions(string[] options, int selectedIndex)
        {
            Console.Clear();
            CenterText("CASHIER SYSTEM");
            CenterText("╔══════════════════════════════════════╗");

            for (int i = 0; i < options.Length; i++)
            {
                string line;
                if (i == selectedIndex)
                {
                    line = $"> {options[i]} <"; 
                }
                else
                {
                    line = $"  {options[i]}";
                }
                CenterText(line);
            }

            CenterText("╚══════════════════════════════════════╝");
            CenterText("Use arrow keys and 'Enter' to select an option");
        }
        private void CenterText(string text)
        {
            int windowWidth = Console.WindowWidth;
            int textWidth = text.Length;
            int padding = (windowWidth - textWidth) / 2;
            Console.WriteLine(new string(' ', Math.Max(padding, 0)) + text);
        }
    }
}
