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
    //Program core
    public class CashierSystemApp
    {
        private readonly Menu.Menu _menuHandler;
        private readonly CustomerManager _customerHandler;
        private readonly AdminManager _adminHandler;

        public CashierSystemApp(Menu.Menu menuHandler, CustomerManager customerHandler, AdminManager adminHandler)
        {
            _menuHandler = menuHandler;
            _customerHandler = customerHandler;
            _adminHandler = adminHandler;
        }

        public void RunApp()
        {
            _menuHandler.RunMenu();

            // Customer

            //Admin settings

            //Close program

        }
    }
}
