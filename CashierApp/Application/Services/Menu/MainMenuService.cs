using CashierApp.Application.Admin;
using CashierApp.Application.Services.Customer;
using CashierApp.Core.Interfaces.Menu;
using CashierApp.Presentation.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashierApp.Application.Services.Menu
{
    /// <summary>
    /// MenuService class, takes care of: Menu logic, UI display, User navigation
    /// </summary>
    public class MainMenuService : IMainMenuService
    {
        private readonly CustomerService _customerService;
        private readonly MenuDisplay _menuDisplay;
        private readonly MainMenuNavigation _mainMenuNavigation;
        private readonly AdminMenu _adminMenu;
        private readonly string[] _options = { "1. New Customer", "2. Admin Settings", "3. Exit" };

        public MainMenuService(CustomerService customerHandler, MenuDisplay menuDisplay, MainMenuNavigation menuNavigation, AdminMenu adminMenu)
        {
            _adminMenu = adminMenu;
            _customerService = customerHandler;
            _menuDisplay = menuDisplay;
            _mainMenuNavigation = menuNavigation;
        }

        /// <summary>
        /// User stays in the menu until they selected "Exit"
        /// </summary>
        public void RunMenu()
        {
            int selectedIndex;
            do
            {
                selectedIndex = _mainMenuNavigation.MainMenuUserNavigation(_options, DisplayMainMenu);
                UserChoises(selectedIndex);
            }
            while (selectedIndex != 2);
        }

        /// <summary>
        /// Displays the main menu options 
        /// </summary>
        /// <param name="index"></param>
        private void DisplayMainMenu(int index)
        {
            _menuDisplay.ShowMenuOptions(_options, index);
        }

        /// <summary>
        /// Manages the user menu choises, and delegates further into the program depending on their 'choise'
        /// </summary>
        /// <param name="selectedIndex"></param>
        private void UserChoises(int selectedIndex)
        {
            switch (selectedIndex)
            {
                case 0:
                    Console.WriteLine("You selected: New Customer\n");
                    _customerService.HandleCustomer();
                    if (_customerService.IsReturningToMenu)
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Exiting...");
                        selectedIndex = 2;
                    }

                    break;
                case 1:
                    Console.WriteLine("You selected: Admin Settings\n");
                    _adminMenu.DisplayAdminMenu();

                    break;
                case 2:
                    Console.WriteLine("Exiting...");
                    break;
                default:
                    Console.WriteLine("Invalid selection.");
                    break;
            }
        }
    }
}
