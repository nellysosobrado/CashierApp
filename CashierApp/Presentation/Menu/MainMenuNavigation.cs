﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashierApp.Presentation.Menu
{
    /// <summary>
    /// Manages navigation within the main menu of the cashier system.
    /// Allows the user to cycle through menu options using arrow keys
    /// and select an option with the Enter key.
    /// </summary>
    public class MainMenuNavigation
    {
        public int _selectedIndex { get; private set; } = 0;
        public int MainMenuUserNavigation(string[] options, Action<int> displayMenu)
        {
            int selectedIndex = 0;
            while (true)
            {
                displayMenu(selectedIndex);

                var key = Console.ReadKey(true).Key;

                if (key == ConsoleKey.UpArrow)
                {
                    selectedIndex = selectedIndex == 0 ? options.Length - 1 : selectedIndex - 1;
                }
                else if (key == ConsoleKey.DownArrow)
                {
                    selectedIndex = selectedIndex == options.Length - 1 ? 0 : selectedIndex + 1;
                }
                else if (key == ConsoleKey.Enter)
                {
                    return selectedIndex;
                }
            }
        }
    }
}
