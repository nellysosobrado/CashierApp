using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashierApp
{
    public class CashierApp
    {
        private readonly MenuManager _menuHandler;
        private readonly CustomerManager _customerHandler;
        private readonly AdminManager _adminHandler;

        public CashierApp(MenuManager menuHandler, CustomerManager customerHandler, AdminManager adminHandler)
        {
            _menuHandler = menuHandler;
            _customerHandler = customerHandler;
            _adminHandler = adminHandler;
        }

        public void Run()
        {
            while (true)
            {
                string choice = _menuHandler.ShowMainMenu();
                if (choice == "1")
                {
                    _customerHandler.HandleCustomer();
                }
                else if (choice == "2")
                {
                    _adminHandler.HandleAdmin();
                }
                else if (choice == "exit")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid choice, please try again.");
                }
            }
        }
    }
}
