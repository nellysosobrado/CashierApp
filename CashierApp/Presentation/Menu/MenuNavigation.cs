using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashierApp.Presentation.Menu
{
    public class MenuNavigation
    {
        public int SelectedIndex { get; private set; } = 0;

        public int UserNavigation(string[] options, Action<int> displayMenu)
        {
            ConsoleKey key;

            do
            {

                displayMenu(SelectedIndex);

                key = Console.ReadKey(true).Key;

                if (key == ConsoleKey.UpArrow)
                {

                    if (SelectedIndex == 0)
                    {
                        SelectedIndex = options.Length - 1;
                    }
                    else
                    {
                        SelectedIndex--;
                    }
                }
                else if (key == ConsoleKey.DownArrow)
                {

                    if (SelectedIndex == options.Length - 1)
                    {
                        SelectedIndex = 0;
                    }
                    else
                    {
                        SelectedIndex++;
                    }
                }

            } while (key != ConsoleKey.Enter);

            return SelectedIndex;
        }
    }
}
