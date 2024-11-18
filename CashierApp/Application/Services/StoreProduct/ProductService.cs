﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using CashierApp.Core.Factories;
using CashierApp.Core.Entities;
using CashierApp.Application.Services.Campaigns;
using CashierApp.Core.Interfaces.Product;

//using CashierApp.Customer;
//using CashierApp.ErrorManagement;


namespace CashierApp.Application.Services.StoreProduct
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
        //DEScription-------------
        public void UpdateProductCampaignDescription(int productId, string newDescription)
        {
            var products = LoadProducts();

            // Hitta produkten
            var product = products.FirstOrDefault(p => p.ProductID == productId);

            if (product == null)
            {
                Console.WriteLine($"ERROR: Product with ID {productId} does not exist.");
                Console.WriteLine("Press any key to continue");
                Console.ReadKey();
                return;
            }

            // Kontrollera om kampanjer finns
            if (product.Campaigns == null || !product.Campaigns.Any())
            {
                Console.WriteLine("No campaigns found for this product.");
                Console.ReadKey();
                return;
            }

            // Uppdatera beskrivningen för den första kampanjen (eller annan logik)
            product.Campaigns.First().Description = newDescription;

            SaveProducts(products);

            Console.WriteLine($"Campaign description for product ID {productId} has been updated to: {newDescription}");
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
        }
        //-----------------------------
        public void DisplayAllProducts()
        {
            Console.Clear();
            Console.WriteLine("LIST OF ALL PRODUCTS (Sorted by ID)");
            Console.WriteLine("--------------------------------------------------");

            var products = LoadProducts().OrderBy(p => p.ProductID).ToList();

            if (!products.Any())
            {
                Console.WriteLine("No products found.");
                Console.WriteLine("Press any key to return to the menu.");
                Console.ReadKey();
                return;
            }

            foreach (var product in products)
            {
                Console.WriteLine($"ID: {product.ProductID}");
                Console.WriteLine($"Name: {product.ProductName}");
                Console.WriteLine($"Category: {product.Category}");
                Console.WriteLine($"Price: {product.Price:C}");
                Console.WriteLine($"Price Type: {product.PriceType}");

                // Visa aktiva kampanjer
                foreach (var campaign in product.Campaigns)
                {
                    Console.WriteLine($" - Campaign: {campaign.Description}");
                    Console.WriteLine($"   Price: {campaign.CampaignPrice:C}");
                    Console.WriteLine($"   Valid: {campaign.StartDate:yyyy-MM-dd} to {campaign.EndDate:yyyy-MM-dd}");
                }

                Console.WriteLine("--------------------------------------------------");
            }

            Console.WriteLine("Press any key to return to the menu.");
            Console.ReadKey();
        }

        public void UpdateProduct(IProducts product)
        {
            var products = LoadProducts();
            var existingProduct = products.FirstOrDefault(p => p.ProductID == product.ProductID);

            if (existingProduct != null)
            {
                existingProduct.ProductName = product.ProductName;
                existingProduct.Price = product.Price;
                existingProduct.PriceType = product.PriceType;
                existingProduct.Category = product.Category;

                // Uppdatera kampanjer om det behövs
                existingProduct.Campaigns = product.Campaigns;

                SaveProducts(products);
            }
            else
            {
                Console.WriteLine($"ERROR: Product with ID {product.ProductID} not found.");
            }
        }

        // Add a new product to the JSON file
        public void AddProduct(string category, int productId, string productName, decimal price, string priceType)
        {
            var products = LoadProducts();

            if (products.Any(p => p.ProductID == productId))
            {
                Console.WriteLine("ERROR: Product already exists");
                Console.WriteLine("Press any key to continue");
                Console.ReadKey();
                return;
            }

            var newProduct = ProductFactory.CreateProduct(category, productId, productName, price, priceType);

            products.Add(newProduct);

            SaveProducts(products.OrderBy(p => p.ProductID).ToList());

            Console.WriteLine($"Product with ID {productId} added successfully.");
        }

        // Update the name of a product in the JSON file
        public void UpdateProductName(int productId, string newName)
        {
            var products = LoadProducts();

            var product = products.FirstOrDefault(p => p.ProductID == productId);

            if (product == null)
            {
                Console.WriteLine($"ERROR: Product with ID {productId} does not exist.");
                return;
            }

            product.ProductName = newName;

            SaveProducts(products);

            Console.WriteLine($"Product ID {productId} has been updated to {newName}.");
        }
        public void UpdateProductPrice(int productId, decimal newPrice)
        {
            var products = LoadProducts();

            var product = products.FirstOrDefault(p => p.ProductID == productId);

            if (product == null)
            {
                Console.WriteLine($"ERROR: Product with ID {productId} does not exist.");
                return;
            }

            product.Price = newPrice;

            SaveProducts(products);

            Console.WriteLine($"Product ID {productId} price updated to {newPrice:C}.");
        }

        public void RemoveProduct(int productId)
        {
            var products = LoadProducts();

            var product = products.FirstOrDefault(p => p.ProductID == productId);

            if (product == null)
            {
                Console.WriteLine($"ERROR: Product with ID {productId} does not exist.");
                Console.WriteLine("Press any key to continue");
                Console.ReadKey();
                return;
            }

            products.Remove(product);

            SaveProducts(products);

            Console.WriteLine($"Product with ID {productId} has been successfully removed.");
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
        }

        //----------------------------------------------------------


        public IProducts GetProductByName(string productName)
        {
            // Load all products from JSON and find the one with the matching name
            return LoadProducts().FirstOrDefault(p => p.ProductName.Equals(productName, StringComparison.OrdinalIgnoreCase));
        }


        public IProducts GetProductById(int productId)
        {
            // Load all products from JSON and find the one with the matching ID
            return LoadProducts().FirstOrDefault(p => p.ProductID == productId);
        }


        public IEnumerable<string> GetDistinctCategories()
        {

            return LoadProducts().Select(p => p.Category).Distinct();
        }


        public IEnumerable<IProducts> GetProductsByCategory(string category)
        {

            return LoadProducts().Where(p => p.Category.Equals(category, StringComparison.OrdinalIgnoreCase));
        }


        public bool CategoryExists(string categoryName)
        {

            return LoadProducts().Any(p => p.Category.Equals(categoryName, StringComparison.OrdinalIgnoreCase));

        }

        //------------------------------------------ JSON

        private List<Product> LoadProducts()
        {
            try
            {
                var json = File.ReadAllText(FilePath);
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var products = JsonSerializer.Deserialize<List<Product>>(json, options) ?? new List<Product>();

                products = products.OrderBy(p => p.ProductID).ToList();

                return products;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR: Failed to load products. Details: {ex.Message}");
                return new List<Product>();
            }
        }



        private void SaveProducts(List<Product> products)
        {
            try
            {
                var options = new JsonSerializerOptions { WriteIndented = true };
                var json = JsonSerializer.Serialize(products, options);
                File.WriteAllText(FilePath, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR: Failed to save products. Details: {ex.Message}");
            }
        }

        public void AddCampaignToProduct(int productId, Campaign campaign)
        {

            var products = LoadProducts();


            var product = products.FirstOrDefault(p => p.ProductID == productId);

            if (product == null)
            {
                Console.WriteLine($"ERROR: Product with ID {productId} does not exist.");
                Console.ReadKey();
                return;
            }


            product.Campaigns.Add(campaign);


            SaveProducts(products);

            Console.WriteLine($"Campaign added to product ID {productId}.");
            Console.ReadKey();
        }
        public List<IProducts> GetAllProducts()
        {
            var products = LoadProducts();
            return products.Cast<IProducts>().ToList();
        }



    }
}
