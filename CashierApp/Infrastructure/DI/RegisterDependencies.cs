using Autofac;
using CashierApp.Application.Admin;
using CashierApp.Application.Factories;
using CashierApp.Application.Services.Campaigns;
using CashierApp.Application.Services.Customer;
using CashierApp.Application.Services.Menu;
using CashierApp.Application.Services.Payment;
using CashierApp.Application.Services.StoreProduct;
using CashierApp.Application.Services.StoreReceipts;
using CashierApp.Infrastructure.ErrorManagement;
using CashierApp.Presentation.Customer;
using CashierApp.Presentation.Menu;
using CashierApp.Presentation.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using CashierApp.Application.Utilities;
using CashierApp.Core.Interfaces.Admin;
using CashierApp.Core.Interfaces.ErrorManagement;
using CashierApp.Core.Interfaces.StoreProducts;
using CashierApp.Core.Interfaces.StoreCampaigns;
using CashierApp.Core.Interfaces.Menu;

namespace CashierApp.Infrastructure.DI
{
    /// <summary>
    /// The DependencyRegister class sets upp all the required dependencies using Autofac, 
    /// in orderr for the application to use dependency injection for managing objects
    /// </summary>
    public static class DependencyRegister
    {
        public static void RegisterDependencies(ContainerBuilder builder)
        {
            //Utility service
            builder.RegisterType<InputValidator>().AsSelf().SingleInstance();

            //product & campaign services.............................................
            builder.RegisterType<AddNewProduct>().As<ICreateProductHandler>().SingleInstance();
            builder.RegisterType<ProductManager>().As<IProductManager>().SingleInstance();
            builder.RegisterType<ProductService>().As<IProductService>().AsSelf().SingleInstance();
            builder.RegisterType<CampaignService>().As<ICampaignManager>().AsSelf().SingleInstance();
            //product & campaign services.............................................

            // Application services......................................................
            builder.RegisterType<MainMenuService>().As<IMainMenuService>().AsSelf().SingleInstance();
            builder.RegisterType<CustomerService>().AsSelf().SingleInstance();
            builder.RegisterType<CustomerInputChecker>().AsSelf().SingleInstance();


            // Presentation layer
            builder.RegisterType<MenuDisplay>().AsSelf().SingleInstance();
            builder.RegisterType<MainMenuNavigation>().AsSelf().SingleInstance();

            //New customer cart
            builder.RegisterType<CartDisplay>().AsSelf().SingleInstance();

            //Display all categories
            builder.RegisterType<ProductDisplay>().AsSelf().SingleInstance();

            //Display products in categories
            builder.RegisterType<ProductCatalog>().AsSelf().SingleInstance();

            // Receipts and payments
            builder.RegisterType<ReceiptService>().AsSelf().SingleInstance();
            builder.RegisterType<PAY>().AsSelf().SingleInstance();

            builder.RegisterType<PriceCalculator>().AsSelf().SingleInstance();

            // Factories
            builder.RegisterType<ReceiptFactory>().AsSelf().SingleInstance();

            // Admin
            builder.RegisterType<AdminMenu>().AsSelf().SingleInstance();
            builder.RegisterType<ProductManager>().As<IProductManager>().SingleInstance();

            // Error management
            builder.RegisterType<Error>().As<IErrorManager>().SingleInstance();

            // Main application
            builder.RegisterType<CashierSystemApp>().AsSelf().SingleInstance();

        }
    }
}
