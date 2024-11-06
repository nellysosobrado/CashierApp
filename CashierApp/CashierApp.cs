using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CashierApp.Admin;
using CashierApp.Customer;
using CashierApp.Menu;

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
            _menuHandler.RunMenu();

            // Customer

            //Admin settings

            //Close program

        }
    }
}
