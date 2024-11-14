using CashierApp.Product.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CashierApp.Product;
using CashierApp.Product.Interfaces;


namespace CashierApp.Admin
{
    public class AdminManager
    {
        private readonly ProductService _productService;
        public AdminManager(ProductService productService)
        {
            _productService = productService;
        }
        
        public void HandleAdmin()
        {
            Console.WriteLine("1. Add Product" +
                "\n2.Edit product" +
                "\n3.Remove product" +
                "\n4.Add campaign" +
                "\n5.Remove campagin");
            string input = Console.ReadLine();

            if (input == "1")
            {
                AddProduct();
            }
            else if (input == "2")
            {
                UpdateProductNameFlow();
            }
            else if (input == "3")
            {
                RemoveProductFlow();
            }
            else if (input == "5") 
            {
                RemoveCampaign();
            }
            else if (input == "4")
            {
                AddCampaign();
            }

        }
        public void AddCampaign()
        {
            Console.WriteLine("ADD CAMPAIGN");

            // Be om produkt-ID
            Console.Write("Enter the Product ID for the campaign: ");
            if (!int.TryParse(Console.ReadLine(), out int productId))
            {
                Console.WriteLine("Invalid Product ID. Please enter a valid number.");
                return;
            }

            // Hämta produkten
            var product = _productService.GetProductById(productId);
            if (product == null)
            {
                Console.WriteLine("Product not found.");
                return;
            }

            // Be om kampanjpriset
            Console.Write("Enter the campaign price: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal campaignPrice))
            {
                Console.WriteLine("Invalid price.");
                return;
            }

            // Be om startdatum
            Console.Write("Enter the start date (yyyy-MM-dd): ");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime startDate))
            {
                Console.WriteLine("Invalid date format.");
                return;
            }

            // Be om slutdatum
            Console.Write("Enter the end date (yyyy-MM-dd): ");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime endDate))
            {
                Console.WriteLine("Invalid date format.");
                return;
            }

            // Sätt kampanjegenskaper
            product.CampaignPrice = campaignPrice;
            product.CampaignStartDate = startDate;
            product.CampaignEndDate = endDate;

            // Uppdatera produkten i datakällan
            _productService.UpdateProduct(product);

            Console.WriteLine($"Campaign added successfully for product {product.ProductName}.");
            Console.ReadKey();
        }
        public void RemoveCampaign()
        {
            Console.WriteLine("REMOVE CAMPAIGN");

            // Be om produkt-ID
            Console.Write("Enter the Product ID for the campaign to remove: ");
            if (!int.TryParse(Console.ReadLine(), out int productId))
            {
                Console.WriteLine("Invalid Product ID. Please enter a valid number.");
                return;
            }

            // Hämta produkten
            var product = _productService.GetProductById(productId);
            if (product == null)
            {
                Console.WriteLine("Product not found.");
                return;
            }

            // Rensa kampanjegenskaper
            product.CampaignPrice = null;
            product.CampaignStartDate = null;
            product.CampaignEndDate = null;
            _productService.UpdateProduct(product);

            Console.WriteLine($"Campaign removed successfully from product {product.ProductName}.");
            Console.ReadKey();
        }

        public void AddProduct()
        {
            Console.WriteLine("ADD NEW PRODUCT");
            Console.Write("Category: ");
            string category = Console.ReadLine();

            Console.Write("ProductID ");
            int productId = int.Parse(Console.ReadLine());

            Console.Write("Product Name");
            string productName = Console.ReadLine();

            Console.Write("Price ");
            decimal price = decimal.Parse(Console.ReadLine());

            Console.Write("PriceType (.'kg', 'piece'): ");
            string priceType = Console.ReadLine();
            if (_productService == null)
            {
                Console.WriteLine("_productService is not initalized.");
                Console.ReadKey();
                return;
            }
            Console.ReadKey();
            _productService.AddProduct(category, productId, productName, price, priceType);
            Console.ReadLine();
        }
        public void UpdateProductNameFlow()
        {
            Console.WriteLine("UPDATE PRODUCT NAME");

 
            Console.Write("Enter the Product ID to update: ");
            if (!int.TryParse(Console.ReadLine(), out int productId))
            {
                Console.WriteLine("Invalid ID. Please enter a valid number.");
                return;
            }

            Console.Write("Enter the new product name: ");
            string newName = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(newName))
            {
                Console.WriteLine("Product name cannot be empty.");
                return;
            }
            if (_productService == null)
            {
                Console.WriteLine("_productService is not initialized.");
                return;
            }

            _productService.UpdateProductName(productId, newName);
        }
        public void RemoveProductFlow()
        {
            Console.WriteLine("REMOVE PRODUCT");

            // Be användaren ange produkt-ID
            Console.Write("Enter the Product ID to remove: ");
            if (!int.TryParse(Console.ReadLine(), out int productId))
            {
                Console.WriteLine("Invalid ID. Please enter a valid number.");
                return;
            }

            // Kontrollera att ProductService är initierat
            if (_productService == null)
            {
                Console.WriteLine("_productService is not initialized.");
                return;
            }

            // Ta bort produkten
            _productService.RemoveProduct(productId);

            //DEBUGG CONTROLL
            //Ifn product exist after removing
            var removedProduct = _productService.GetProductById(productId);
            if(removedProduct != null)
            {
                Console.WriteLine($"\nDEBUG:ERROR {productId} still exists after removal");
                Console.WriteLine("Enter any key to continue to menu.");
                Console.ReadKey();
            }
 

        }

    }
}
