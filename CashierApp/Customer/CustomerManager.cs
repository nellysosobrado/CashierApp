using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CashierApp.Payment.Services;
using CashierApp.Product.Services;

namespace CashierApp.Customer
{
    public class CustomerManager
    {
        private readonly ProductService _productService;
        private readonly PaymentService _paymentService;

        public CustomerManager(ProductService productService, PaymentService paymentService)
        {
            _productService = productService;
            _paymentService = paymentService;
        }

        public void HandleCustomer()
        {
            _productService.ShowProducts();
            Console.WriteLine("Enter product ID and quantity, or 'PAY' to finish:");

            while (true)
            {
                var input = Console.ReadLine()?.Trim();
                if (input?.ToUpper() == "PAY")
                {
                    _paymentService.ProcessPayment();
                    break;
                }
                Console.WriteLine("Product added to cart.");
            }
        }
    }
}
