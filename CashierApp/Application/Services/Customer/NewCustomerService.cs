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
    /// <summary>
    /// CustomerService handles all customer-related actions, including cart management, payment, and product browsing.
    /// </summary>
    public class NewCustomerService
    {
        public bool IsReturningToMenu { get; private set; }
        private readonly CartDisplay _cartDisplay;
        private readonly IErrorManager _errorManager;
        private readonly PAY _pay;
        private readonly ProductCatalog _productCatalog;
        private readonly NewCustomerInputValidator _newCustomerInputValidator;
        private List<(IProducts Product, int Quantity)> _cart = new List<(IProducts Product, int Quantity)>();
        private readonly PriceCalculator _priceCalculator;

        public NewCustomerService
            (PAY paymentService,
            IErrorManager errorManager,
            CartDisplay newCustomer, ProductCatalog productCatalog, NewCustomerInputValidator CustomerInputChecker,
            PriceCalculator priceCalculator
            )
        {
            _priceCalculator = priceCalculator;
            _cartDisplay = newCustomer;
            _pay = paymentService;
            _errorManager = errorManager;
            _productCatalog = productCatalog;
            _newCustomerInputValidator = CustomerInputChecker;
        }

        /// <summary>
        /// Takes care of customers intereaction loop
        /// </summary>
        public void ManageCustomerSession()
        {
            IsReturningToMenu = false;
            while (!IsReturningToMenu)
            {
                _cartDisplay.DisplayCart(_cart);
                var input = Console.ReadLine()?.Trim();
                if (string.IsNullOrEmpty(input))
                {
                    _errorManager.DisplayError("Input cannot be empty. Press any key to try again");
                    Console.ReadKey();
                    continue;
                }
                if (!ProcessCustomerInput(input))//Processs the input and decide which method to start depending on the user input
                {
                    break;
                }
            }
        }
        /// <summary>
        /// Processes the user's input and performs corresponding actions.
        /// </summary>
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

                default://Checks if the input is a product
                    var result = _newCustomerInputValidator.ProcessProductInput(input);
                    if (result.HasValue)
                    {
                        _cart.Add(result.Value);
                        _cartDisplay.DisplayCart(_cart);//Refreshes the cart display
                    }
                    return true;
            }
        }
        /// <summary>
        /// Processes the payment for the items in the cart.
        /// </summary>
        private void ProcessPayment()
        {
            Console.WriteLine("\nProcessing payment...");
            decimal totalPrice = _priceCalculator.CalculateTotalPrice(_cart);
            _pay.ProcessPayment(_cart, totalPrice);
            _cart.Clear();
        }

        /// <summary>
        /// Displays the product catalog to the customer.
        /// </summary>
        private void DisplayProductCatalog()
        {
            _productCatalog.ShowProductCatalog();
            _cartDisplay.DisplayCart(_cart);
        }
        /// <summary>
        /// Clears the cart and sets the flag to return to the main menu.
        /// </summary>
        private void ReturnToMainMenu()
        {
            Console.WriteLine("\nReturning to Main Menu...");
            _cart.Clear();//Clears cart, incasse the user wants to buy again
            IsReturningToMenu = true;
        }
        /// <summary>
        /// Displays an error message for empty input.
        /// </summary>
        private void DisplayEmptyInputError()
        {
            
        }

    }

}
