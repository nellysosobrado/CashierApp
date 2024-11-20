using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CashierApp.Core.Entities;
using CashierApp.Presentation.Menu;
using CashierApp.Application.Admin;
using CashierApp.Core.Interfaces.Admin;
using CashierApp.Core.Interfaces.ErrorManagement;
using CashierApp.Core.Interfaces.StoreProducts;
using CashierApp.Core.Interfaces.StoreCampaigns;

namespace CashierApp.Application.Admin
{
    /// <summary>
    /// AdminMenu manages the user input 
    /// </summary>
    public class AdminMenu : IAdminMenuManager
    {
        private readonly IProductManager _productManager;
        private readonly ICampaignManager _campaignManager;
        private readonly IErrorManager _errorManager;
        private readonly MainMenuNavigation _mainMenuNavigation;
        private readonly string[] _menuOptions = {
        "Create new product",
        "Edit product",
        "Remove product",
        "Add campaign",
        "Remove campaign",
        "View all products",
        "Back to Main menu"
         };

        public AdminMenu(IProductManager productManager, ICampaignManager campaignManager, IErrorManager errorManager)
        {
            _productManager = productManager;
            _campaignManager = campaignManager;
            _errorManager = errorManager;
            _mainMenuNavigation = new MainMenuNavigation();
        }
        private bool UserChoise(int selectedIndex)
        {
            switch (selectedIndex)
            {
                case 0:
                    _productManager.AddNewProduct();
                    break;
                case 1:
                    _productManager.EditProductDetails();
                    break;
                case 2:
                    _productManager.RemoveProduct();
                    break;
                case 3:
                    _campaignManager.AddCampaign();
                    break;
                case 4:
                    _campaignManager.RemoveCampaign();
                    break;
                case 5:
                    _productManager.DisplayProductsAndCampaigns();
                    break;
                case 6:
                    return false; //back to menu
                default:
                    _errorManager.DisplayError("ERROR: Invalid option");
                    break;
            }

            return true; //continutes in the menu
        }

        public void DisplayButtons()
        {
            bool keepRunning = true;

            while (keepRunning)
            {
                Console.Clear();
                

                int selectedIndex = _mainMenuNavigation.MainMenuUserNavigation(_menuOptions, DisplayOptions);

                keepRunning = UserChoise(selectedIndex);
            }
        }
        private void DisplayTitle()
        {
            CenterText("╔═══════════════════════════════════════════════╗");
            CenterText("║                  ADMIN MENU                   ║");
            CenterText("╚═══════════════════════════════════════════════╝");
            CenterText("─────────────────────────────────────────────────");
        }

        /// <summary>
        /// Visar alternativen i menyn med det valda alternativet markerat.
        /// </summary>
        private void DisplayOptions(int selectedIndex)
        {
            Console.Clear();
            DisplayTitle();

            for (int i = 0; i < _menuOptions.Length; i++)
            {
                if (i == selectedIndex)
                {
                    CenterText($"> {_menuOptions[i]} <"); 
                }
                else
                {
                    CenterText($"  {_menuOptions[i]}"); // Visar övriga alternativ
                }
            }

            CenterText("─────────────────────────────────────────────────");
        }

        private void CenterText(string text)
        {
            int windowWidth = Console.WindowWidth;
            int leftPadding = Math.Max((windowWidth - text.Length) / 2, 0);
            Console.SetCursorPosition(leftPadding, Console.CursorTop);
            Console.WriteLine(text);
        }
    }

}
