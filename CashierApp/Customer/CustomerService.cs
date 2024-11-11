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
        private List<(IProducts Product, int Quantity)> _cart = new List<(IProducts Product, int Quantity)>();

        //Constructor
        public CustomerService
            (ProductService productService, PAY paymentService,
            IErrorManager errorManager, ProductDisplay productDisplay,
            CartDisplay newCustomer
            )
        {
            _newCustomer = newCustomer;
            _productService = productService;
            _paymentService = paymentService;
            _errorManager = errorManager;
            _productDisplay = productDisplay;
        }
        public void ShowProductCatalog() //Categories & their products information
        {
            while (true)
            {
                Console.Clear();
                var categories = _productService.GetDistinctCategories();
                _productDisplay.ShowCategories(categories);

                string input = Console.ReadLine()?.Trim().ToLower() ?? string.Empty;

               if (input == "c")
                {
                    return;
                }
                var products = _productService.GetProductsByCategory(input);

                if (products.Any())
                {
                    int pageSize = 5; 
                    int currentPage = 0;
                    bool browsing = true;

                    while (browsing)
                    {
                        _productDisplay.ShowProductsByCategory(products, input, currentPage, pageSize);

                        string command = Console.ReadLine()?.Trim().ToLower() ?? string.Empty;

                        switch (command)
                        {
                            case "n":
                                if ((currentPage + 1) * pageSize < products.Count())
                                {
                                    currentPage++;
                                }
                                else
                                {
                                    Console.WriteLine("You are on the last page. Press any key to continue...");
                                    Console.ReadKey();
                                }
                                break;

                            case "p":
                                if (currentPage > 0)
                                {
                                    currentPage--;
                                }
                                else
                                {
                                    Console.WriteLine("You are on the first page. Press any key to continue...");
                                    Console.ReadKey();
                                }
                                break;

                            case "r":
                                browsing = false;
                                break;

                            case "c":
                                return;

                            default:
                                Console.WriteLine("Invalid command. Press any key to try again...");
                                Console.ReadKey();
                                break;
                        }
                    }
                }
                else
                {
                    _productDisplay.ShowNoProductsMessage(input);
                    Console.ReadKey();
                }
            }
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
                        decimal totalPrice = CalculateTotalPrice(_cart);
                        _paymentService.ProcessPayment(_cart, totalPrice);
                        _cart.Clear();
                        return; 

                    case "1":
                        ShowProductCatalog();
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
        private decimal CalculateTotalPrice(List<(IProducts Product, int Quantity)> cart) //TOTAL PRICE
        {
            decimal total = 0;
            foreach (var item in cart)
            {
                total += item.Product.Price * item.Quantity;
            }
            return total;
        }
        
    }
}
