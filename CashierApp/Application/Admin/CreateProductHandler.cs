﻿using CashierApp.Application.Utilities;
using CashierApp.Core.Entities;
using CashierApp.Core.Interfaces.Admin;
using CashierApp.Core.Interfaces.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashierApp.Application.Admin
{
    public class CreateProductHandler : ICreateProductHandler
    {
        private readonly IProductService _productService;
        private readonly InputValidator _inputValidator;

        public CreateProductHandler(IProductService productService, InputValidator inputValidator)
        {
            _productService = productService;
            _inputValidator = inputValidator;
        }

        public void CreateNewProduct()
        {
            Console.Clear();
            Console.WriteLine("ADD NEW PRODUCT");
            Console.WriteLine("Please enter the product's details for the new product");
            Console.WriteLine("........................................");

            // Skapa en ny produkt och fyll dess egenskaper
            var product = new Product
            {
                Category = _inputValidator.GetValidatedInput(
                    "Category",                             
                    input => !string.IsNullOrWhiteSpace(input),
                    "Category cannot be empty."
                ),
                ProductID = _inputValidator.GetUniqueId(
                    "ProductID",
                    id => id > 0 && _productService.GetProductById(id) == null,
                    "ProductID cannot be 0 or already exist."
                ),
                ProductName = _inputValidator.GetValidatedInput(
                    "Product Name",
                    input => !string.IsNullOrWhiteSpace(input) && _productService.GetProductByName(input) == null,
                    "Product name cannot be empty or already exist."
                ),
                Price = _inputValidator.GetValidatedDecimal(
                    "Price",
                    price => price > 0,
                    "Invalid price. Must be greater than 0."
                ),
                PriceType = _inputValidator.GetValidatedInput(
                    "PriceType (e.g., 'kg', 'piece')",
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
                Console.WriteLine($"An error occurred while adding the product: {ex.Message}");
            }

            Console.WriteLine("Press any key to return to the Admin menu.");
            Console.ReadKey();
        }
    }
}
