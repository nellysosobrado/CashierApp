using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CashierApp.Menu;
using CashierApp.Payment.Services;
using CashierApp.Product.Interfaces;
using CashierApp.Product.Services;


namespace CashierApp.Customer
{
    public class CustomerManager
    {
        private readonly ProductService _productService;
        private readonly PaymentService _paymentService;
        private List<(IProducts Product, int Quantity)> _cart = new List<(IProducts Product, int Quantity)>();

        public CustomerManager(ProductService productService, PaymentService paymentService)
        {
            _productService = productService;
            _paymentService = paymentService;
   
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
                    
                   
                    _productService.ShowCategories(); 
      
                }
                else if (input == "2")
                {
                    Console.WriteLine("\nReturning to Main Menu...");
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
                        if (product != null)
                        {
                            Console.Clear();
                            _cart.Add((product, quantity));
                          
                            ShowCart(); 
                        }
                        else
                        {
                            Console.WriteLine("\nProduct not found. Please enter a valid product ID.");
                        }
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("\nInvalid input. Please enter product ID and quantity (e.g., '101 2').");
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
