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
    public class MenuService : IMenuService
    {
        private readonly CustomerService _customerHandler;
        private readonly MenuDisplay _menuDisplay;
        private readonly MenuNavigation _menuNavigation;
        private readonly AdminMenu _adminMenu;
        private readonly string[] _options = { "1. New Customer", "2. Admin Settings", "3. Exit" };

        public MenuService(CustomerService customerHandler, MenuDisplay menuDisplay, MenuNavigation menuNavigation, AdminMenu adminMenu)
        {
            _adminMenu = adminMenu;
            _customerHandler = customerHandler;
            _menuDisplay = menuDisplay;
            _menuNavigation = menuNavigation;
        }
        public void RunMenu()
        {
            int selectedIndex;
            do
            {
                selectedIndex = _menuNavigation.UserNavigation(_options, DisplayMenuWithSelection);
                UserSelectedOption(selectedIndex);
            }
            while (selectedIndex != 2);
        }
        private void DisplayMenuWithSelection(int index)
        {
            _menuDisplay.ShowMenuOptions(_options, index);
        }
        private void UserSelectedOption(int selectedIndex)
        {
            switch (selectedIndex)
            {
                case 0:
                    Console.WriteLine("You selected: New Customer\n");
                    _customerHandler.HandleCustomer();
                    if (_customerHandler.IsReturningToMenu)
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
