using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CashierApp.Presentation.Products;
using CashierApp.Application.Services.Campaigns;
using CashierApp.Core.Entities;
using CashierApp.Core.Interfaces.StoreProducts;

namespace CashierApp.Presentation.Products
{
    //Display Products
    public class ProductDisplay
    {
        private readonly CampaignService _campaignManager;

        public ProductDisplay(CampaignService campaignManager)
        {
            _campaignManager = campaignManager;
        }
        public void ShowCategories(IEnumerable<string> categories) //CATEGORIES
        {
            Console.Clear();
            Console.WriteLine("\n                                   ╔═══════════════════════════════════════════════╗");
            Console.WriteLine("                                   ║                AVAILABLE CATEGORIES           ║");
            Console.WriteLine("                                   ╚═══════════════════════════════════════════════╝");
            Console.WriteLine("                                   ────────────────────────────────────────────────");

            foreach (var category in categories)
            {
                CenterText($"- {category}");
            }

            Console.WriteLine("                                   ────────────────────────────────────────────────");
            CenterText("Enter a category to view its products");
            Console.Write("                                                      Command: ");
        }

        public void ShowProductsByCategory(IEnumerable<IProducts> products, string category, int currentPage, int pageSize) //PRODUCTS, after categoires
        {
            Console.Clear();
            Console.WriteLine();
            CenterText($"{category.ToUpper()}");

            CenterText("───────────────────────────────────────────────");
            CenterText($"Page {currentPage + 1}");
            CenterText("ID    │ Product Name           │   Price   │");
            CenterText("───────────────────────────────────────────────");

            foreach (var product in products)
            {
                var activeCampaigns = _campaignManager.GetActiveCampaigns(product.Campaigns);
                if (activeCampaigns.Any())
                {
                    var campaign = activeCampaigns.First(); // Använd den första aktiva kampanjen
                    var productPriceWithCampaign = product.Price - campaign.CampaignPrice;

                    // Produkt med kampanj
                    CenterText($"{product.ProductID,-5} │ {product.ProductName.PadRight(25)} │ {productPriceWithCampaign,10:C2}");
                }
                else
                {
                    // Produkt utan kampanj
                    CenterText($"{product.ProductID,-5} │ {product.ProductName.PadRight(25)} │ {product.Price,10:C2}");
                }
            }
            Console.WriteLine("                                   ────────────────────────────────────────────────");
            CenterText("[N] Next page  [P] Previous page [C] Return to cart  [R] Return to categories  ");
            Console.Write("                                                      Command: ");
        }
        //Console.Write("                                                      Command: ");
        private void CenterText(string text) //Design
        {
            int windowWidth = Console.WindowWidth;
            int leftPadding = (windowWidth - text.Length) / 2;
            if (leftPadding < 0) leftPadding = 0;

            Console.SetCursorPosition(leftPadding, Console.CursorTop);
            Console.WriteLine(text);
        }
    }
}
