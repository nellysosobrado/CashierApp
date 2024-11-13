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
using System.Text.Json;

namespace CashierApp.Product.Services
{
    /// <summary>
    /// Manages the productdata 
    /// </summary>
    public class ProductService : IProductService
    {
        private readonly string folderPath = "../../../AddedProducts"; // where it saves
        private readonly string FilePath;
        private readonly List<IProducts> _products;

        public ProductService()
        {
            //Filepath!! IT combines
            FilePath = Path.Combine(folderPath, "products.json");

            //creates a map
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            _products = LoadProducts();

            if (_products == null || !_products.Any())
            {
                _products = new List<IProducts>
            {
                ProductFactory.CreateProduct("fruit & greens", 1, "Banana", 1.99m, "piece"),
                ProductFactory.CreateProduct("fruit & greens", 2, "Orange", 1.99m, "piece"),
                ProductFactory.CreateProduct("fruit & greens", 3, "Apple", 1.99m, "piece"),
                ProductFactory.CreateProduct("meat", 4, "Chicken", 5.99m, "kg")
            };

                SaveProducts();
            }
        }

        public void AddProduct(string category, int productId, string productName, decimal price, string priceType)
        {
            if (_products.Any(p => p.ProductID == productId))
            {
                Console.WriteLine("En produkt med samma ID finns redan.");
                return;
            }

            var newProduct = ProductFactory.CreateProduct(category, productId, productName, price, priceType);
            _products.Add(newProduct);
            SaveProducts();
            Console.WriteLine($"Produkten {productName} har lagts till.");
        }

        private void SaveProducts()
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            options.Converters.Add(new ProductConverter());
            var json = JsonSerializer.Serialize(_products, options);
            File.WriteAllText(FilePath, json);
        }

        private List<IProducts> LoadProducts()
        {
            if (File.Exists(FilePath))
            {
                var options = new JsonSerializerOptions();
                options.Converters.Add(new ProductConverter());
                var json = File.ReadAllText(FilePath);
                return JsonSerializer.Deserialize<List<IProducts>>(json, options) ?? new List<IProducts>();
            }
            return new List<IProducts>();
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

        public bool CategoryExists(string categoryName)
        {
            return _products.Any(p => p.Category.Equals(categoryName, StringComparison.OrdinalIgnoreCase));
        }
    }
}
