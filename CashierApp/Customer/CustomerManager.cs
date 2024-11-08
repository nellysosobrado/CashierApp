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
        private readonly NewCustomer _newCustomer;
        private readonly ProductDisplay _productDisplay;
        private readonly IErrorManager _errorManager;
        private readonly ProductService _productService;
        private readonly PAY _paymentService;
        private List<(IProducts Product, int Quantity)> _cart = new List<(IProducts Product, int Quantity)>();

        public CustomerManager(ProductService productService, PAY paymentService, IErrorManager errorManager, ProductDisplay productDisplay, NewCustomer newCustomer)
        {
            _newCustomer = newCustomer;
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
            Console.WriteLine($"Found {categories.Count()} categories.");
            _productDisplay.ShowProductsByCategory(products, selectedCategory);
            _newCustomer.ShowCart(_cart);
            Console.ReadLine();
        }
       
        public void HandleCustomer()
        {

            while (true)
            {
               
                //Console.ReadKey();
                //Console.Clear(); 
                _newCustomer.ShowCart(_cart);

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
                    //Console.Clear();
                    ShowProductCategoriesAndProducts();
                    _newCustomer.ShowCart(_cart);
                    Console.ReadLine();

                }
                else if (input == "2")
                {
                    Console.WriteLine("\nReturning to Main Menu...");
                    _cart.Clear();
                    break;
                }
                else
                {
                    var parts = input?.Split(' ');
                    if (parts?.Length == 2 && int.TryParse(parts[0], out int productId) && int.TryParse(parts[1], out int quantity))
                    {
                        var product = _productService.GetProductById(productId);
                        if (product != null) 
                        {
                            _cart.Add((product, quantity)); 
                            _newCustomer.ShowCart(_cart); 
                        }
                        else 
                        {
                            _errorManager.DisplayError("\nProduct ID not found. Please enter a valid product ID");
                        }
                    }
                    else
                    {
                        _errorManager.DisplayError("\nInvalid input. Please enter product ID and quantity.");
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
