using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using CashierApp.Customer;
using CashierApp.Product.Factories;
using CashierApp.Product.Interfaces;
using CashierApp.ErrorManagement;

namespace CashierApp.Product.Services
{
    /// <summary>
    /// Manages the productdata 
    /// </summary>
    public class ProductService
    {
        private readonly List<IProducts> _products;

        public ProductService() // Products stored as a list
        {
            _products = new List<IProducts>
            {
                ProductFactory.CreateProduct("fruit & greens", 1, "Banana", 1.99m, "piece"),
                ProductFactory.CreateProduct("fruit & greens", 2, "Orange", 1.99m, "piece"),
                ProductFactory.CreateProduct("fruit & greens", 3, "Apple", 1.99m, "piece"),
                ProductFactory.CreateProduct("fruit & greens", 4, "Caesar Sallad", 1.99m, "piece"),
                ProductFactory.CreateProduct("fruit & greens", 5, "Onion", 1.99m, "piece"),
                ProductFactory.CreateProduct("fruit & greens", 6, "Garlic", 1.99m, "piece"),
                ProductFactory.CreateProduct("fruit & greens", 7, "Ginger", 1.99m, "piece"),
                ProductFactory.CreateProduct("fruit & greens", 8, "Grapes", 1.99m, "piece"),
                ProductFactory.CreateProduct("fruit & greens", 9, "Clementines", 1.99m, "piece"),
                ProductFactory.CreateProduct("fruit & greens", 10,"Mushroom", 1.49m, "piece"),
                ProductFactory.CreateProduct("meat", 11, "Chicken", 5.99m, "kg"),
                ProductFactory.CreateProduct("drink", 12, "Milk", 1.49m, "liter"),
                ProductFactory.CreateProduct("bakery", 13, "Bread", 2.49m, "piece"),
                ProductFactory.CreateProduct("dairy", 14, "Cheese", 3.99m, "kg"),
                ProductFactory.CreateProduct("bajskorv", 15, "bajs", 0m, "piece")
            };
        }
        public IProducts GetProductByName(string productName)
        {
            return _products.FirstOrDefault(p => p.ProductName.Equals(productName, StringComparison.OrdinalIgnoreCase));
        }

        public IProducts GetProductById(int productId)
        {
            return _products.Find(p => p.ProductID == productId); 
        }

        public IEnumerable<string> GetDistinctCategories()
        {
            return _products.Select(p => p.Category).Distinct();
        }

        public IEnumerable<IProducts> GetProductsByCategory(string category)
        {
            return _products.Where(p => p.Category.Equals(category, StringComparison.OrdinalIgnoreCase));
        }
    }
}
