using CashierApp.Admin;
using CashierApp.Customer;
using CashierApp.ErrorManagement;
using CashierApp.Menu.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashierApp.Menu
{
    //Menu Logic
    //UI Menu
    //Menu Navigation
    public class MenuService : IMenuService
    {
        private readonly CustomerService _customerHandler;
        private readonly AdminManager _adminHandler;
        private readonly MenuDisplay _menuDisplay;
        private readonly MenuNavigation _menuNavigation;
        private readonly string[] _options = { "1. New Customer", "2. Admin Settings", "3. Exit" };

       
        public MenuService(CustomerService customerHandler, AdminManager adminHandler, MenuDisplay menuDisplay, MenuNavigation menuNavigation)
        {
            _customerHandler = customerHandler;
            _adminHandler = adminHandler;
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
            Console.Clear();

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
                    _adminHandler.HandleAdmin();
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
