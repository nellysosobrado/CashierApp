using CashierApp.Admin;
using CashierApp.Customer;
using CashierApp.ErrorManagement;
using CashierApp.Payment;
using CashierApp.Product;
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
        private readonly IErrorManager _errorManager;

        // Konstruktor för att injicera IErrorManager
        public MenuFactory(IErrorManager errorManager)
        {
            _errorManager = errorManager;
        }
        public ProductService CreateProductService()
        {
            return new ProductService();
        }

        public static PAY CreatePaymentService()
        {
            return new PAY();
        }

        public CustomerService CreateCustomerManager(IErrorManager errorManager)
        {
            var productService = new ProductService();
            var paymentService = CreatePaymentService();
            var productDisplay = new ProductDisplay();
            var newCustomer = new CartDisplay();
            var productCatalog = new ProductCatalog(productService,productDisplay);
            var customerInputChecker = new CustomerInputChecker(productService, errorManager);
            return new CustomerService(productService, paymentService, _errorManager, productDisplay, newCustomer, productCatalog, customerInputChecker);
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
