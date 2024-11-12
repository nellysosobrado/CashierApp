using CashierApp.Product.Interfaces;
using CashierApp.Product.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashierApp.ErrorManagement
{
    public delegate IProducts ProductSearchDelegate(string input);

    public class CustomerInputChecker
    {
        private readonly ProductService _productService;
        private readonly IErrorManager _errorManager;

        public CustomerInputChecker(ProductService productService, IErrorManager errorManager)
        {
            _productService = productService;
            _errorManager = errorManager;
        }

        /// <summary>
        /// Processar användarens produktinmatning, validerar format och letar efter produkten.
        /// </summary>
        public (IProducts Product, int Quantity)? ProcessProductInput(string input)
        {
            var parts = input.Split(' ');

            if (!IsValidInput(parts))
            {
                _errorManager.DisplayError("Invalid input. Enter product ID/name followed by quantity.");
                Console.ReadKey();
                return null;
            }

            int quantity = int.Parse(parts[1]);

            if (quantity == 0)
            {
                _errorManager.DisplayError("Quantity cannot be zero. Please enter a valid quantity.");
                Console.ReadKey();
                return null;
            }

            ProductSearchDelegate searchMethod = DetermineSearchMethod(parts[0]);
            var product = searchMethod(parts[0]);

            if (product == null)
            {
                _errorManager.DisplayError("Product does not exist. Press any key to try again");
                Console.ReadKey();
                return null;
            }

            return (product, quantity);
        }

        private bool IsValidInput(string[] parts)
        {
            return parts.Length == 2 && int.TryParse(parts[1], out _);
        }

        private ProductSearchDelegate DetermineSearchMethod(string input)
        {
            return int.TryParse(input, out _) ? SearchById : SearchByName;
        }

        private IProducts SearchById(string input)
        {
            int productId = int.Parse(input);
            return _productService.GetProductById(productId);
        }

        private IProducts SearchByName(string input)
        {
            return _productService.GetProductByName(input.ToLower());
        }
    }
}
