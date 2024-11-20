using CashierApp.Application.Services.Campaigns;
using CashierApp.Core.Interfaces.StoreProducts;

namespace CashierApp.Presentation.Products
{
    /// <summary>
    /// ProductDisplay class Handles displaying product categories and products to the user
    /// </summary>
    public class ProductDisplay
    {
        private readonly CampaignService _campaignManager;

        public ProductDisplay(CampaignService campaignManager)
        {
            _campaignManager = campaignManager;
        }
        public void ShowCategories(IEnumerable<string> categories) 
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
            CenterText("[C] back to cart");

            Console.WriteLine("                                   ────────────────────────────────────────────────");
            CenterText("Enter a category to view its products");
            Console.Write("                                                      Command: ");
        }
        public void ShowProductsByCategory(IEnumerable<IProducts> products, string category, int currentPage, int pageSize) 
        {
            Console.Clear();
            Console.WriteLine();
            CenterText($"{category.ToUpper()}");

            CenterText("───────────────────────────────────────────────");
            CenterText($"Page {currentPage + 1}");
            CenterText("ID    │ Product Name           │   Price   │");
            CenterText("───────────────────────────────────────────────");

            foreach (var product in products)// Display each category in the center of the screen
            {
                var activeCampaigns = _campaignManager.GetActiveCampaigns(product.Campaigns);
                if (activeCampaigns.Any())
                {
                    var campaign = activeCampaigns.First(); 
                    var productPriceWithCampaign = product.Price - campaign.CampaignPrice;

                    CenterText($"{product.ProductID,-5} │ {product.ProductName.PadRight(25)} │ {productPriceWithCampaign,10:C2}");
                }
                else
                {
                    CenterText($"{product.ProductID,-5} │ {product.ProductName.PadRight(25)} │ {product.Price,10:C2}");
                }
            }
            Console.WriteLine("                                   ────────────────────────────────────────────────");
            CenterText("[N] Next page  [P] Previous page [C] Return to cart  [R] Return to categories  ");
            Console.Write("                                                      Command: ");
        }
        private void CenterText(string text) 
        {
            int windowWidth = Console.WindowWidth;
            int leftPadding = (windowWidth - text.Length) / 2;
            if (leftPadding < 0) leftPadding = 0;

            Console.SetCursorPosition(leftPadding, Console.CursorTop);
            Console.WriteLine(text);
        }
    }
}
