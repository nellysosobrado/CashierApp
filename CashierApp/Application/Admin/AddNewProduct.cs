using CashierApp.Application.Utilities;
using CashierApp.Core.Entities;
using CashierApp.Core.Interfaces.Admin;
using CashierApp.Core.Interfaces.ErrorManagement;
using CashierApp.Core.Interfaces.StoreProducts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashierApp.Application.Admin
{
    /// <summary>
    /// AddNewProduct takes care of the function to add a new product into the application
    /// </summary>
    public class AddNewProduct : ICreateProductHandler
    {
        private readonly IProductService _productService;
        private readonly InputValidator _inputValidator;
        private readonly IErrorManager _errorManager;

        public AddNewProduct(IProductService productService, InputValidator inputValidator, IErrorManager errorManager)
        {
            _productService = productService;
            _inputValidator = inputValidator;
            _errorManager = errorManager;
        }

        /// <summary>
        /// Adds a new product to the system by collecting and checking user input
        /// </summary>
        public void AddProduct()
        {
            Console.Clear();
            Console.WriteLine("ADD NEW PRODUCT");
            Console.WriteLine("Please enter the product's details for the new product");
            Console.WriteLine("........................................");


            var product = new Product// Create a new product and collect its properties
            {
                Category = _inputValidator.GetValidatedInput(
                    "Category",                            
                    input => !string.IsNullOrWhiteSpace(input),
                    "Category cannot be empty."
                ),
                ProductID = _inputValidator.GetUniqueId(
                    "ProductID",
                    id => id > 0 && _productService.GetProductId(id) == null,
                    "ProductID cannot be 0 or already exist."
                ),
                ProductName = _inputValidator.GetValidatedInput(
                    "Product Name",
                    input => !string.IsNullOrWhiteSpace(input) && _productService.GetProductName(input) == null,
                    "Product name cannot be empty or already exist."
                ),
                Price = _inputValidator.GetValidatedDecimal(
                    "Price",
                    price => price > 0,
                    "Invalid price. Must be greater than 0."
                ),
                PriceType = _inputValidator.GetValidatedInput(
                    "PriceType ('kg', 'piece')",
                    input => input.Equals("kg", StringComparison.OrdinalIgnoreCase) || input.Equals("piece", StringComparison.OrdinalIgnoreCase),
                    "Invalid PriceType. Must be 'kg' or 'piece'."
                )
            };
            try
            {
                _productService.AddProduct(product.Category, product.ProductID, product.ProductName, product.Price, product.PriceType);
                Console.WriteLine("Product successfully added!");
            }
            catch (Exception ex)
            {
                _errorManager.DisplayError($"An error occurred while adding the product: {ex.Message}");
            }
            Console.WriteLine("Press any key to return to the Admin menu.");
            Console.ReadKey();
        }
    }
}
