using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CashierApp.Application.Admin;
using CashierApp.Application.Services.Customer;
using CashierApp.Application.Services.Menu;

namespace CashierApp
{
    /// <summary>
    /// Starts the Cashier Application
    /// </summary>
    public class CashierSystemApp
    {
        private readonly MainMenuService _menuHandler;

        public CashierSystemApp(MainMenuService menuHandler)
        {
            _menuHandler = menuHandler;
        }

        /// <summary>
        /// RunApp() initates the program, by calling RunMenu method from the MenuService class
        /// </summary>
        public void RunApp()
        {
            _menuHandler.RunMenu();
        }
    }
}
