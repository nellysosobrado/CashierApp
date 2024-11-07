using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            _productService.ShowProducts();

            while (true)
            {
                Console.Write("\nEnter 'PAY' to finish" +
                "\nCommand:");
                var input = Console.ReadLine()?.Trim();
                if (input?.ToUpper() == "PAY")
                {
                    Console.WriteLine("Processing payment");

                   
                    decimal totalPrice = CalculateTotalPrice(_cart);

                    _paymentService.ProcessPayment(_cart, totalPrice); //Displays cart and total price

                    _cart.Clear(); //resetes cart


                    break;
                }

                var parts = input?.Split(' ');
                if (parts?.Length == 2 && int.TryParse(parts[0], out int productId) && int.TryParse(parts[1], out int quantity))
                {
                    var product = _productService.GetProductById(productId);
                    if (product != null)
                    {
                        _cart.Add((product, quantity));
                        Console.WriteLine("Product added to cart.");
                        ShowCart();
                    }
                    else
                    {
                        Console.WriteLine("Product not found. Please enter a valid product ID.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter product ID and quantity.");
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

        private void ShowCart()
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
