using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CashierApp.Application.Admin;
using CashierApp.Application.Services;

namespace CashierApp
{
    //Program core, focuses on the program flow
    public class CashierSystemApp
    {
        private readonly MenuService _menuHandler;
        private readonly CustomerService _customerHandler;
        private readonly AdminMenu _adminHandler;

        public CashierSystemApp(MenuService menuHandler, CustomerService customerHandler, AdminMenu adminHandler)
        {
            _menuHandler = menuHandler;
            _customerHandler = customerHandler;
            _adminHandler = adminHandler;
        }

        public void RunApp()
        {
            _menuHandler.RunMenu();

        }
    }
}
