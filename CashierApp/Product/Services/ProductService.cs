using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using CashierApp.Customer;
using CashierApp.Product.Factories;
using CashierApp.Product.Interfaces;

namespace CashierApp.Product.Services
{
    public class ProductService
    {
        private List<IProducts> _products;

        public IProducts GetProductById(int productId)
        {
            
            return _products.Find(p => p.ProductID == productId);
        }

        public ProductService()
        {
            
            _products = new List<IProducts>
        {
            ProductFactory.CreateProduct("fruit", 101, "Apple", 1.99m, "piece"),
            ProductFactory.CreateProduct("fruit", 102, "Orange", 1.49m, "piece"),
            ProductFactory.CreateProduct("meat", 103, "Chicken", 5.99m, "kg"),
            ProductFactory.CreateProduct("drink", 104, "Milk", 1.49m, "liter"),
            ProductFactory.CreateProduct("bakery", 105, "Bread", 2.49m, "piece"),
            ProductFactory.CreateProduct("dairy", 106, "Cheese", 3.99m, "kg"),
            ProductFactory.CreateProduct("bajskorv", 107, "bajs", 0m, "piece")
        };
        }
        public void ShowCategories()
        {
            Console.Clear();
            Console.WriteLine("\nAvailable Categories:");

            var categories = _products.Select(p => p.Category).Distinct();

            foreach (var category in categories)
            {
                Console.WriteLine($"- {category}");
            }

            Console.Write("\nEnter a category to view its products" +
                "\n>Commando:");
            string selectedCategory = Console.ReadLine()?.Trim().ToLower();

            ShowProductsByCategory(selectedCategory);
        }

        public void ShowProductsByCategory(string category)
        {
            // Filtrera produkter baserat på den valda kategorin
            var productsInCategory = _products.Where(p => p.Category.ToLower() == category);

            if (productsInCategory.Any())
            {
                Console.Clear();
                Console.WriteLine($"\nProducts in category '{category}':");
                Console.WriteLine("───────────────────────────────────────────────");

                foreach (var product in productsInCategory)
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

        //public void ShowProducts()
        //{
        //    Console.WriteLine("Product List by Category:");

        //    // Gruppera produkterna efter kategori och visa dem
        //    var groupedProducts = _products.GroupBy(p => p.Category);

        //    foreach (var group in groupedProducts)
        //    {
        //        Console.WriteLine($"\nCategory: {group.Key}");
        //        Console.WriteLine("───────────────────────────────────────────────");

        //        foreach (var product in group)
        //        {
        //            //Console.WriteLine("───────────────────────────────────────────────");
        //            Console.WriteLine($"\n Name      : {product.Name}");
        //            Console.WriteLine($" Product ID: {product.ProductID}");
        //            Console.WriteLine($" Price     : {product.Price:C}");
        //            Console.WriteLine($" Unit      : {product.PriceType}");
        //           // Console.WriteLine("───────────────────────────────────────────────\n");
        //        }
        //    }
        //}
    }
}
