using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CashierApp.Application.Admin;
using CashierApp.Presentation.Menu;
using CashierApp.Presentation.Customer;
using CashierApp.Presentation.Products;
using CashierApp.Application.Services.Customer;
using CashierApp.Application.Services.Payment;
using CashierApp.Application.Services.StoreProduct;
using CashierApp.Application.Services.Campaigns;
using CashierApp.Core.Interfaces.ErrorManagement;
using CashierApp.Core.Interfaces.StoreProducts;
using CashierApp.Core.Interfaces.StoreCampaigns;

namespace CashierApp.Application.Factories
{
    /// <summary>
    /// MenuFactory creates and sets up services 
    /// </summary>
    public class MenuFactory
    {
        private readonly IErrorManager _errorManager;

        /// <summary>
        /// Constructor to inject IErrorManager
        /// </summary>
        /// <param name="errorManager"></param>
        public MenuFactory(IErrorManager errorManager)
        {
            _errorManager = errorManager;
        }

        /// <summary>
        /// Create a ProductService instance
        /// </summary>
        /// <returns></returns>
        public ProductService CreateProductService()
        {
            return new ProductService();
        }

        /// <summary>
        /// Create a PaymentService (PAY) instance
        /// </summary>
        /// <returns></returns>
        public PAY CreatePaymentService()
        {
            var productService = CreateProductService();
            var campaignService = new CampaignService(productService);
            return new PAY(campaignService);
        }

        /// <summary>
        /// Create a CustomerService instance
        /// </summary>
        /// <returns></returns>
        public CustomerService CreateCustomerService()
        {
            var productService = CreateProductService();
            var paymentService = CreatePaymentService();
            var campaignService = new CampaignService(productService);

            var productDisplay = new ProductDisplay(campaignService);
            var cartDisplay = new CartDisplay(campaignService, _errorManager);
            var productCatalog = new ProductCatalog(productService, productDisplay);
            var customerInputChecker = new CustomerInputChecker(productService, _errorManager);

            return new CustomerService(productService, paymentService, _errorManager, productDisplay, cartDisplay, productCatalog, customerInputChecker);
        }

        /// <summary>
        /// Create an AdminMenu instance
        /// </summary>
        /// <param name="productManager"></param>
        /// <param name="campaignManager"></param>
        /// <returns></returns>
        public AdminMenu CreateAdminMenu(IProductManager productManager, ICampaignManager campaignManager)
        {
            return new AdminMenu(productManager, campaignManager, _errorManager);
        }

        /// <summary>
        /// Create a MenuDisplay instance
        /// </summary>
        /// <returns></returns>
        public MenuDisplay CreateMenuDisplay()
        {
            return new MenuDisplay();
        }
    }
}
