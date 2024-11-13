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
                "\n2.Edit product");
            string input = Console.ReadLine();

            if (input == "1")
            {
                AddProduct();
            }
            else if (input == "2")
            {
                UpdateProductNameFlow();
            }

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

    }
}
