using CashierApp.Admin;
using CashierApp.Customer;
using CashierApp.Payment.Services;
using CashierApp.Product.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashierApp.Menu
{
    public class MenuFactory
    {
        public static ProductService CreateProductService()
        {
            return new ProductService();
        }

        public static PaymentService CreatePaymentService()
        {
            return new PaymentService();
        }

        public static CustomerManager CreateCustomerManager()
        {
            var productService = CreateProductService();
            var paymentService = CreatePaymentService();
            return new CustomerManager(productService, paymentService);
        }

        public static AdminManager CreateAdminManager()
        {
            return new AdminManager();
        }

        public static MenuDisplay CreateMenuDisplay()
        {
            return new MenuDisplay();
        }

        public static MenuNavigation CreateMenuNavigation()
        {
            return new MenuNavigation();
        }
    }
}
