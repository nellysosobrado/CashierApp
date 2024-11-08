using CashierApp.Product.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashierApp.Product
{
    public class ProductDisplay
    {
        public void ShowCategories(IEnumerable<string> categories)
        {
            Console.WriteLine("\nAvailable Categories:");
            foreach (var category in categories)
            {
                Console.WriteLine($"- {category}");
            }
        }

        public void ShowProductsByCategory(IEnumerable<IProducts> products, string category)
        {
            if (products.Any())
            {
                Console.Clear();
                Console.WriteLine($"\nProducts in category '{category}':");
                Console.WriteLine("───────────────────────────────────────────────");

                foreach (var product in products)
                {
                    Console.WriteLine($"\n Name      : {product.Name}");
                    Console.WriteLine($" Product ID: {product.ProductID}");
                    Console.WriteLine($" Price     : {product.Price:C}");
                    Console.WriteLine($" Unit      : {product.PriceType}");
                }
            }
            else
            {
                Console.WriteLine($"\nNo products found in the category '{category}'.");
            }
        }
    }
}
