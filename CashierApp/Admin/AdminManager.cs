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
            Console.WriteLine($"Product {productName} has been added.");
            Console.ReadLine();
        }
    }
}
