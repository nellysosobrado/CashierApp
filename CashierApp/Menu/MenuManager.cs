using CashierApp.Admin;
using CashierApp.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace CashierApp.Menu
{
    //Menu Logic
    //UI Menu
    //Menu Navigation
    public class MenuManager
    {
        private readonly CustomerManager _customerHandler;
        private readonly AdminManager _adminHandler;
        private readonly MenuDisplay _menuDisplay;
        private readonly MenuNavigation _menuNavigation;
        private readonly string[] _options = { "1. New Customer", "2. Admin Settings", "3. Exit" };

        public MenuManager(CustomerManager customerHandler, AdminManager adminHandler)
        {
            _customerHandler = customerHandler;
            _adminHandler = adminHandler;
            _menuDisplay = new MenuDisplay();
            _menuNavigation = new MenuNavigation();
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

            Console.WriteLine("Program exiting...");
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
