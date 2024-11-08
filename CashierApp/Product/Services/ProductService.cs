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
    public class ProductService
    {
        private readonly List<IProducts> _products;

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
