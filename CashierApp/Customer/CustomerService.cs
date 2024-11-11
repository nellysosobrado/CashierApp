using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CashierApp.Menu;
using CashierApp.Payment;
using CashierApp.Product.Interfaces;
using CashierApp.Product.Services;
using CashierApp.ErrorManagement;
using CashierApp.Product;


namespace CashierApp.Customer
{
    public class CustomerService
    {
        private readonly CartDisplay _newCustomer;
        private readonly ProductDisplay _productDisplay;
        private readonly IErrorManager _errorManager;
        private readonly ProductService _productService;
        private readonly PAY _paymentService;
        private readonly ProductCatalog _productCatalog;
        private List<(IProducts Product, int Quantity)> _cart = new List<(IProducts Product, int Quantity)>();

        //Constructor
        public CustomerService
            (ProductService productService, PAY paymentService,
            IErrorManager errorManager, ProductDisplay productDisplay,
            CartDisplay newCustomer, ProductCatalog productCatalog
            )
        {
            _newCustomer = newCustomer;
            _productService = productService;
            _paymentService = paymentService;
            _errorManager = errorManager;
            _productDisplay = productDisplay;
            _productCatalog = productCatalog;
        }
             
        public void HandleCustomer() //NEW CUSTOMER
        {
            while (true)
            {
                _newCustomer.ShowCart(_cart);
                var input = Console.ReadLine()?.Trim();

                if (string.IsNullOrEmpty(input))
                {
                    _errorManager.DisplayError("Input cannot be empty. Press any key to try again");
                    Console.ReadKey();
                    continue;
                }

                switch (input?.ToUpper())
                {
                    case "PAY":
                        Console.WriteLine("\nProcessing payment...");
                        decimal totalPrice = PriceCalculator.CalculateTotalPrice(_cart);
                        _paymentService.ProcessPayment(_cart, totalPrice);
                        _cart.Clear();
                        
                        return; 

                    case "1":
                        _productCatalog.ShowProductCatalog();
                        _newCustomer.ShowCart(_cart);

                        break;

                    case "2":
                        Console.WriteLine("\nReturning to Main Menu...");
                        _cart.Clear();
                        return; 

                    default:
                        var parts = input.Split(' ');
                        if (parts.Length == 2 && int.TryParse(parts[1], out int quantity))
                        {
                            if (int.TryParse(parts[0], out int productId))
                            {
                                var product = _productService.GetProductById(productId);
                                if (product != null)
                                {
                                    _cart.Add((product, quantity));
                                    _newCustomer.ShowCart(_cart);
                                }
                                else
                                {
                                    _errorManager.DisplayError("Product ID does not exist. Press any key to try again");
                                    Console.ReadKey();
                                }
                            }
                            else
                            {
                                var productName = parts[0].ToLower();
                                var product = _productService.GetProductByName(productName);
                                if (product != null)
                                {
                                    _cart.Add((product, quantity));
                                    _newCustomer.ShowCart(_cart);
                                }
                                else
                                {
                                    _errorManager.DisplayError("Product name does not exist. Press any key to try again");
                                    Console.ReadKey();
                                }
                            }
                        }
                        else
                        {
                            _errorManager.DisplayError("Invalid input. Please enter a valid product ID or name followed by quantity. Press any key to try again");
                            Console.ReadKey();
                        }
                        break;
                }
            }
        }

    }
}
