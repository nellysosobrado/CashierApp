using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using CashierApp.Product.Factories;
using CashierApp.Product.Interfaces;

namespace CashierApp.Product.Services
{
    public class ProductService
    {
        private List<IProducts> _products;

        public IProducts GetProductById(int productId)
        {
            // Hitta och returnera produkten med angivet ProductID
            return _products.Find(p => p.ProductID == productId);
        }

        public ProductService()
        {
            // Skapa produkter med hjälp av ProductFactory
            _products = new List<IProducts>
        {
            ProductFactory.CreateProduct("fruit", 101, "Apple", 1.99m, "piece"),
            ProductFactory.CreateProduct("fruit", 102, "Orange", 1.49m, "piece"),
            ProductFactory.CreateProduct("meat", 103, "Chicken", 5.99m, "kg"),
            ProductFactory.CreateProduct("drink", 104, "Milk", 1.49m, "liter"),
            ProductFactory.CreateProduct("bakery", 105, "Bread", 2.49m, "piece"),
            ProductFactory.CreateProduct("dairy", 106, "Cheese", 3.99m, "kg")
        };
        }

        public void ShowProducts()
        {
            Console.WriteLine("Product List by Category:");

            // Gruppera produkterna efter kategori och visa dem
            var groupedProducts = _products.GroupBy(p => p.Category);

            foreach (var group in groupedProducts)
            {
                Console.WriteLine($"\nCategory: {group.Key}");

                foreach (var product in group)
                {
                    Console.WriteLine("───────────────────────────────────────────────");
                    Console.WriteLine($" Product ID: {product.ProductID}");
                    Console.WriteLine($" Name      : {product.Name}");
                    Console.WriteLine($" Price     : {product.Price:C}");
                    Console.WriteLine($" Unit      : {product.PriceType}");
                    Console.WriteLine("───────────────────────────────────────────────\n");
                }
            }
        }
    }
}
