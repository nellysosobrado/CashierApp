using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CashierApp.Core.Interfaces;
using CashierApp.Core.Entities;

//using CashierApp.Product;
//using CashierApp.Product.Interfaces;
//using CashierApp.Product.Services;

namespace CashierApp.Application.Admin
{
    public class AdminMenu : IAdminMenuManager
    {
        private readonly IProductManager _productManager;
        private readonly ICampaignManager _campaignManager;

        public AdminMenu(IProductManager productManager, ICampaignManager campaignManager)
        {
            _productManager = productManager;
            _campaignManager = campaignManager;
        }

        public void DisplayAdminMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Page: Admin Menu");
                Console.WriteLine("\nPRODUCTS" +
                    "\n1. Create new product" +
                    "\n2. Edit product" +
                    "\n3. Remove product" +
                    "\n" +
                    "\nCAMPAIGNS" +
                    "\n4. Add campaign" +
                    "\n5. Remove campaign" +
                    "\n6. View all products" +
                    "\n" +
                    "\n7. Back to Main menu");
                Console.WriteLine();
                Console.Write(">");

                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        _productManager.CreateNewProduct();
                        break;
                    case "2":
                        _productManager.EditProductDetails();
                        break;
                    case "3":
                        _productManager.RemoveProduct();
                        break;
                    case "4":
                        _campaignManager.AddCampaign();
                        break;
                    case "5":
                        _campaignManager.RemoveCampaign();
                        break;
                    case "6":
                        _productManager.DisplayProductsAndCampaigns();
                        break;
                    case "7":
                        return;
                    default:
                        Console.WriteLine("Invalid input. Try again.");
                        break;
                }
            }
        }
    }

}
