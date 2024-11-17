using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CashierApp.Core.Interfaces;
using CashierApp.Application.Admin;
using CashierApp.Application.Services;
using CashierApp.Presentation.Menu;
using CashierApp.Presentation.Customer;
using CashierApp.Presentation.Products;


//using CashierApp.ErrorManagement;
//using CashierApp.Product;
//using CashierApp.Campaigns;


namespace CashierApp.Application.Factories
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

            var campaignManager = new CampaignService(productService);
            var productDisplay = new ProductDisplay(campaignManager);
            var newCustomer = new CartDisplay(campaignManager);
            var productCatalog = new ProductCatalog(productService, productDisplay);
            var customerInputChecker = new CustomerInputChecker(productService, errorManager);
            return new CustomerService(productService, paymentService, _errorManager, productDisplay, newCustomer, productCatalog, customerInputChecker);
        }

        public AdminMenu CreateAdminMenu(IProductManager productManager, ICampaignManager campaignManager)
        {
            return new AdminMenu(productManager, campaignManager);
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
