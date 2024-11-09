using CashierApp.Product.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashierApp.Product
{
    //Display Products
    public class ProductDisplay
    {
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
            Console.WriteLine("\n                                   ╔═══════════════════════════════════════════════╗");
            Console.WriteLine($"                                   ║       PRODUCTS IN CATEGORY: '{category.ToUpper()}'       ║");
            Console.WriteLine("                                   ╚═══════════════════════════════════════════════╝");
            Console.WriteLine("                                   ────────────────────────────────────────────────");
            Console.WriteLine($"                                   Page {currentPage + 1}");
            Console.WriteLine("                                    ID    │ Product ProductName        │   Price   │   Unit");
            Console.WriteLine("                                   ────────────────────────────────────────────────");

            var pagedProducts = products.Skip(currentPage * pageSize).Take(pageSize);
            foreach (var product in pagedProducts)
            {
                string productName = product.ProductName.Length > 17 ? product.ProductName.Substring(0, 17) + "…" : product.ProductName;
                Console.WriteLine($"                                    {product.ProductID,-5} │ {productName,-17} │ {product.Price,8:C} │ {product.PriceType}");
            }

            Console.WriteLine("                                   ────────────────────────────────────────────────");
            CenterText("[N] Next page  [P] Previous page  ");
            CenterText("[C] Return to cart  [R] Return to categories");
            Console.Write("                                                      Command: ");
        }

        public void ShowNoProductsMessage(string category) //If there's no products
        {
            Console.Clear();
            Console.WriteLine("\n                                   ╔═══════════════════════════════════════════════╗");
            Console.WriteLine($"                                   ║      NO PRODUCTS IN CATEGORY: '{category.ToUpper()}'     ║");
            Console.WriteLine("                                   ╚═══════════════════════════════════════════════╝");
            Console.WriteLine("                                   ────────────────────────────────────────────────");
            CenterText("Press any key to return to categories...");
        }

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
