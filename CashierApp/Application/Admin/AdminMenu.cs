using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CashierApp.Core.Interfaces;
using CashierApp.Core.Entities;
using CashierApp.Presentation.Menu;

namespace CashierApp.Application.Admin
{
    public class AdminMenu : IAdminMenuManager
    {
        private readonly IProductManager _productManager;
        private readonly ICampaignManager _campaignManager;
        private readonly IErrorManager _errorManager;
        private readonly MenuNavigation _menuNavigation;

        public AdminMenu(IProductManager productManager, ICampaignManager campaignManager, IErrorManager errorManager)
        {
            _productManager = productManager;
            _campaignManager = campaignManager;
            _errorManager = errorManager;
            _menuNavigation = new MenuNavigation();
        }

        public void DisplayAdminMenu()
        {
            
            bool keepRunning = true;
            string[] options = {
                "Create new product",
                "Edit product",
                "Remove product",
                "Add campaign",
                "Remove campaign",
                "View all products",
                "Back to Main menu"
            };

            while (keepRunning)
            {
                Console.Clear();
                DisplayTitle();

                int selectedIndex = _menuNavigation.UserNavigation(options, DisplayOptions);

                keepRunning = HandleCommand(selectedIndex);
            }
        }

        private void DisplayTitle()
        {
            CenterText("╔═══════════════════════════════════════════════╗");
            CenterText("║                  ADMIN MENU                   ║");
            CenterText("╚═══════════════════════════════════════════════╝");
            CenterText("─────────────────────────────────────────────────");
        }

        private void DisplayOptions(int selectedIndex)
        {
            Console.Clear();
            string[] options = {
                "Create new product",
                "Edit product",
                "Remove product",
                "Add campaign",
                "Remove campaign",
                "View all products",
                "Back to Main menu"
            };

            for (int i = 0; i < options.Length; i++)
            {
                if (i == selectedIndex)
                {
                    CenterText($"> {options[i]} <"); // Markera valt alternativ
                }
                else
                {
                    CenterText($"  {options[i]}");
                }
            }

            CenterText("─────────────────────────────────────────────────");
        }

        private bool HandleCommand(int selectedIndex)
        {
            switch (selectedIndex)
            {
                case 0:
                    _productManager.CreateNewProduct();
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
                    return false; // Gå tillbaka till huvudmenyn
                default:
                    _errorManager.DisplayError("ERROR: Invalid option");
                    break;
            }

            return true; // Fortsätt i menyn
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
