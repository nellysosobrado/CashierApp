using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CashierApp.Application.Services.Payment;
using CashierApp.Application.Services.StoreProduct;
using CashierApp.Core.Interfaces.StoreProducts;
using CashierApp.Core.Interfaces.ErrorManagement;
using CashierApp.Core.Interfaces.StoreCampaigns;
using CashierApp.Presentation.Customer;
using CashierApp.Presentation.Products;
using CashierApp.Core.Entities;

namespace CashierApp.Application.Services.Customer
{
    public class CustomerService
    {
        private readonly PriceCalculator _priceCalculator;
        public bool IsReturningToMenu { get; private set; }
        private readonly CartDisplay _newCustomer;
        private readonly ProductDisplay _productDisplay;
        private readonly IErrorManager _errorManager;
        private readonly ProductService _productService;
        private readonly PAY _paymentService;
        private readonly ProductCatalog _productCatalog;
        private readonly CustomerInputChecker _CustomerInputChecker;
        private List<(IProducts Product, int Quantity)> _cart = new List<(IProducts Product, int Quantity)>();

        public CustomerService
            (ProductService productService, PAY paymentService,
            IErrorManager errorManager, ProductDisplay productDisplay,
            CartDisplay newCustomer, ProductCatalog productCatalog, CustomerInputChecker CustomerInputChecker,
            PriceCalculator priceCalculator
            )
        {
            _priceCalculator = priceCalculator;
            _newCustomer = newCustomer;
            _productService = productService;
            _paymentService = paymentService;
            _errorManager = errorManager;
            _productDisplay = productDisplay;
            _productCatalog = productCatalog;
            _CustomerInputChecker = CustomerInputChecker;
        }
        public void HandleCustomer()
        {
            IsReturningToMenu = false;

            while (!IsReturningToMenu)
            {
                _newCustomer.DisplayCart(_cart);
                var input = Console.ReadLine()?.Trim();

                if (string.IsNullOrEmpty(input))
                {
                    DisplayEmptyInputError();
                    continue;
                }

                if (!ProcessCustomerInput(input))
                {
                    break;
                }
            }
        }

        private bool ProcessCustomerInput(string input)
        {
            switch (input.ToUpper())
            {
                case "PAY":
                    ProcessPayment();
                    return false;

                case "1":
                    DisplayProductCatalog();
                    return true;

                case "2":
                    ReturnToMainMenu();
                    return false;

                default:
                    var result = _CustomerInputChecker.ProcessProductInput(input);
                    if (result.HasValue)
                    {
                        _cart.Add(result.Value);
                        _newCustomer.DisplayCart(_cart);
                    }
                    return true;
            }
        }

        private void ProcessPayment()
        {
            Console.WriteLine("\nProcessing payment...");
            decimal totalPrice = _priceCalculator.CalculateTotalPrice(_cart);
            _paymentService.ProcessPayment(_cart, totalPrice);
            _cart.Clear();
        }

        private void DisplayProductCatalog()
        {
            _productCatalog.ShowProductCatalog();
            _newCustomer.DisplayCart(_cart);
        }

        private void ReturnToMainMenu()
        {
            Console.WriteLine("\nReturning to Main Menu...");
            _cart.Clear();
            IsReturningToMenu = true;
        }

        private void DisplayEmptyInputError()
        {
            _errorManager.DisplayError("Input cannot be empty. Press any key to try again");
            Console.ReadKey();
        }

    }

}
