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
    /// Manages product data using a json file as the main source
    /// </summary>
    public class ProductService : IProductService
    {
        private readonly string folderPath = "../../../AddedProducts"; // Path to save the products
        private readonly string FilePath; // Full file path for the JSON file

        public ProductService()
        {
            // Combine folder path and file name
            FilePath = Path.Combine(folderPath, "products.json");

            // Create the folder if it doesn't exist
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            // Ensure the JSON file exists
            if (!File.Exists(FilePath))
            {
                File.WriteAllText(FilePath, "[]"); // Create an empty JSON array
            }
        }

        // Add a new product to the JSON file
        public void AddProduct(string category, int productId, string productName, decimal price, string priceType)
        {
            // Load all products from JSON
            var products = LoadProducts();

            // Check if a product with the same ID already exists
            if (products.Any(p => p.ProductID == productId))
            {
                Console.WriteLine("ERROR: Product already exists");
                Console.WriteLine("Press any key to continue");
                Console.ReadKey();
                return;
            }

            // Create a new product
            var newProduct = ProductFactory.CreateProduct(category, productId, productName, price, priceType);

            // Add the new product to the list
            products.Add(newProduct);

            // Save the updated list back to JSON
            SaveProducts(products);

            Console.WriteLine($"Product {productName} has been added.");
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
        }

        // Update the name of a product in the JSON file
        public void UpdateProductName(int productId, string newName)
        {
            // Load all products from JSON
            var products = LoadProducts();

            // Find the product by ID
            var product = products.FirstOrDefault(p => p.ProductID == productId);

            // If the product doesn't exist, show an error
            if (product == null)
            {
                Console.WriteLine($"ERROR: Product with ID {productId} does not exist.");
                Console.WriteLine("Press any key to continue");
                Console.ReadKey();
                return;
            }

            // Update the product name
            product.ProductName = newName;

            // Save the updated list back to JSON
            SaveProducts(products);

            Console.WriteLine($"Product ID {productId} has been updated to {newName}.");
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
        }

        // Remove a product from the JSON file
        public void RemoveProduct(int productId)
        {
            // Load all products from JSON
            var products = LoadProducts();

            // Find the product by ID
            var product = products.FirstOrDefault(p => p.ProductID == productId);

            // If the product doesn't exist, show an error
            if (product == null)
            {
                Console.WriteLine($"ERROR: Product with ID {productId} does not exist.");
                Console.WriteLine("Press any key to continue");
                Console.ReadKey();
                return;
            }


            // Remove the product
            products.Remove(product);

            // Save the updated list back to JSON
            SaveProducts(products);

            Console.WriteLine($"Product ID {productId} has been removed.");
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
        }

        // Get a product by name
        public IProducts GetProductByName(string productName)
        {
            // Load all products from JSON and find the one with the matching name
            return LoadProducts().FirstOrDefault(p => p.ProductName.Equals(productName, StringComparison.OrdinalIgnoreCase));
        }

        // Get a product by ID
        public IProducts GetProductById(int productId)
        {
            // Load all products from JSON and find the one with the matching ID
            return LoadProducts().FirstOrDefault(p => p.ProductID == productId);
        }

        // Get all distinct categories
        public IEnumerable<string> GetDistinctCategories()
        {
            // Load all products from JSON and return distinct categories
            return LoadProducts().Select(p => p.Category).Distinct();
        }

        // Get all products in a specific category
        public IEnumerable<IProducts> GetProductsByCategory(string category)
        {
            // Load all products from JSON and filter by category
            return LoadProducts().Where(p => p.Category.Equals(category, StringComparison.OrdinalIgnoreCase));
        }

        // Check if a category exists
        public bool CategoryExists(string categoryName)
        {
            // Load all products from JSON and check if the category exists
            return LoadProducts().Any(p => p.Category.Equals(categoryName, StringComparison.OrdinalIgnoreCase));
        }

        // Load all products from the JSON file
        private List<IProducts> LoadProducts()
        {
            var json = File.ReadAllText(FilePath);
            var options = new JsonSerializerOptions();
            options.Converters.Add(new ProductConverter()); // Add custom converter for products
            return JsonSerializer.Deserialize<List<IProducts>>(json, options) ?? new List<IProducts>();
        }

        // Save all products to the JSON file
        private void SaveProducts(List<IProducts> products)
        {
            var options = new JsonSerializerOptions { WriteIndented = true }; // Make JSON easier to read
            var json = JsonSerializer.Serialize(products, options);
            File.WriteAllText(FilePath, json); // Write JSON to the file
        }
    }
}
