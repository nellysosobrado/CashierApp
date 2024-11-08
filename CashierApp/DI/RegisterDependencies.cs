using Autofac;
using CashierApp.Admin;
using CashierApp.Customer;
using CashierApp.ErrorManagement;
using CashierApp.Menu;
using CashierApp.Payment;
using CashierApp.Product;
using CashierApp.Product.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashierApp.DI
{
    public static class DependencyRegister
    {
        public static void RegisterDependencies(ContainerBuilder builder)
        {
            //Register the dependencies, so they can injiac
            //'.AsSelf()' Instances will be accesabe for DI as their own type
            builder.RegisterType<Error>().As<IErrorManager>().SingleInstance();
            builder.RegisterType<ProductService>().AsSelf();
            builder.RegisterType<PAY>().AsSelf();
            builder.RegisterType<CustomerManager>().AsSelf();
            builder.RegisterType<AdminManager>().AsSelf();
            builder.RegisterType<MenuDisplay>().AsSelf();
            builder.RegisterType<MenuNavigation>().AsSelf();
            builder.RegisterType<MenuManager>().AsSelf();
            builder.RegisterType<CashierSystemApp>().AsSelf();
            builder.RegisterType<ProductDisplay>().AsSelf();
            builder.RegisterType<NewCustomer>().AsSelf();


        }
    }
}
