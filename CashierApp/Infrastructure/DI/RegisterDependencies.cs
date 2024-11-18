using Autofac;
using CashierApp.Application.Admin;
using CashierApp.Application.Factories;
using CashierApp.Application.Services.Campaigns;
using CashierApp.Application.Services.Customer;
using CashierApp.Application.Services.Menu;
using CashierApp.Application.Services.Payment;
using CashierApp.Application.Services.StoreProduct;
using CashierApp.Application.Services.Receipts;
using CashierApp.Core.Interfaces;
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

namespace CashierApp.Infrastructure.DI
{
    public static class DependencyRegister
    {
        public static void RegisterDependencies(ContainerBuilder builder)
        {
            // Core services
            builder.RegisterType<ProductService>().As<IProductService>().AsSelf().SingleInstance();
            builder.RegisterType<CampaignService>().As<ICampaignManager>().AsSelf().SingleInstance();

            // Application services
            builder.RegisterType<MenuService>().As<IMenuService>().AsSelf().SingleInstance();
            builder.RegisterType<CustomerService>().AsSelf().SingleInstance();
            builder.RegisterType<ProductCatalog>().AsSelf().SingleInstance();
            builder.RegisterType<CustomerInputChecker>().AsSelf().SingleInstance();

            // Presentation layer
            builder.RegisterType<MenuDisplay>().AsSelf().SingleInstance();
            builder.RegisterType<MenuNavigation>().AsSelf().SingleInstance();
            builder.RegisterType<CartDisplay>().AsSelf().SingleInstance();
            builder.RegisterType<ProductDisplay>().AsSelf().SingleInstance();

            // Receipts and payments
            builder.RegisterType<ReceiptService>().AsSelf().SingleInstance();
            builder.RegisterType<PAY>().AsSelf().SingleInstance();

            // Factories
            builder.RegisterType<ReceiptFactory>().AsSelf().SingleInstance();

            // Admin
            builder.RegisterType<AdminMenu>().AsSelf().SingleInstance();
            builder.RegisterType<ProductManager>().As<IProductManager>().SingleInstance();

            // Error management
            builder.RegisterType<Error>().As<IErrorManager>().SingleInstance();

            // Main application
            builder.RegisterType<CashierSystemApp>().AsSelf().SingleInstance();

            // Debugging callback (optional for troubleshooting)
            builder.RegisterBuildCallback(container =>
            {
                var campaignManager = new CampaignService(new ProductService()); // Exempel
                PriceCalculator.SetCampaignManager(campaignManager);

                Console.WriteLine("Dependencies registered and PriceCalculator initialized.");
            });


        }


    }
}
