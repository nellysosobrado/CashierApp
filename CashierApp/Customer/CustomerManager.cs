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
    public class CustomerManager
    {
        private readonly ProductDisplay _productDisplay;
        private readonly IErrorManager _errorManager;
        private readonly ProductService _productService;
        private readonly PAY _paymentService;
        private List<(IProducts Product, int Quantity)> _cart = new List<(IProducts Product, int Quantity)>();

        public CustomerManager(ProductService productService, PAY paymentService, IErrorManager errorManager, ProductDisplay productDisplay)
        {

            _productService = productService;
            _paymentService = paymentService;
            _errorManager = errorManager;
            _productDisplay = productDisplay;
        }
        public void ShowProductCategoriesAndProducts()
        {
            var categories = _productService.GetDistinctCategories();
            _productDisplay.ShowCategories(categories);

            Console.Write("\nEnter a category to view its products\n>Commando: ");
            string selectedCategory = Console.ReadLine()?.Trim().ToLower();
            var products = _productService.GetProductsByCategory(selectedCategory);
            _productDisplay.ShowProductsByCategory(products, selectedCategory);
        }
        public void HandleCustomer()
        {
            while (true)
            {
                Console.WriteLine("\n1. Product Registry" +
                                  "\n2. Main Menu" +
                                  "\n3. Exit");

                //Console.Write("\nEnter 'PAY' to finish or choose an option:\nCommand: ");
                Console.Write("\n>Commando:");
                var input = Console.ReadLine()?.Trim();

                if (input?.ToUpper() == "PAY")
                {
                    Console.WriteLine("\nProcessing payment...");

                    decimal totalPrice = CalculateTotalPrice(_cart);
                    _paymentService.ProcessPayment(_cart, totalPrice); 

                    _cart.Clear(); 
                    break;
                }
                else if (input == "1")
                {

                    Console.Clear();
                    ShowProductCategoriesAndProducts();
                    ShowCart();


                }
                else if (input == "2")
                {
                    Console.WriteLine("\nReturning to Main Menu...");
                    _cart.Clear();
                    //Return to main menu
                    break; 
                }
                else if (input == "3")
                {
                    Console.WriteLine("\nExiting...");
                    Environment.Exit(0); 
                }
                else
                {
                
                    var parts = input?.Split(' ');
                    if (parts?.Length == 2 && int.TryParse(parts[0], out int productId) && int.TryParse(parts[1], out int quantity))
                    {
                        var product = _productService.GetProductById(productId);

                        if (product != null)// product is not empty
                        {
                            //Console.Clear();
                            _cart.Add((product, quantity));
                          
                            ShowCart(); 
                        }
                        else//if does not find
                        {
                            _errorManager.DisplayError("\nProduct ID not found. Please enter a valid product ID");
                        }
                    }
                    else
                    {
                        //Console.Clear();
                        _errorManager.DisplayError("\nInvalid input. Please enter product ID and quantity. Example: '302 1'");
                        ShowCart();
                        
                    }
                }
            }
        }

        private decimal CalculateTotalPrice(List<(IProducts Product, int Quantity)> cart)
        {
            decimal total = 0;
            foreach (var item in cart)
            {
                total += item.Product.Price * item.Quantity;
            }
            return total;
        }
        public void ShowCart()
        {
            Console.WriteLine("\nCurrent cart:");
            foreach (var item in _cart)
            {
                Console.WriteLine($"Product: {item.Product.Name}, Quantity: {item.Quantity}, Total: {item.Product.Price * item.Quantity:C}");
            }
            
            Console.WriteLine();
        }
    }
}
