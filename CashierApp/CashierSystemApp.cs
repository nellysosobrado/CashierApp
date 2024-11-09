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
    //Program core, focuses on the program flow
    public class CashierSystemApp
    {
        private readonly Menu.MenuService _menuHandler;
        private readonly CustomerService _customerHandler;
        private readonly AdminManager _adminHandler;

        public CashierSystemApp(Menu.MenuService menuHandler, CustomerService customerHandler, AdminManager adminHandler)
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
