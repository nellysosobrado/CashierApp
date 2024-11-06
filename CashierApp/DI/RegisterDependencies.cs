using Autofac;
using CashierApp.Admin;
using CashierApp.Customer;
using CashierApp.Menu;
using CashierApp.Payment.Services;
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
            builder.RegisterType<MenuManager>().AsSelf();
            builder.RegisterType<CustomerManager>().AsSelf();
            builder.RegisterType<AdminManager>().AsSelf();
            builder.RegisterType<ProductService>().AsSelf();
            builder.RegisterType<PaymentService>().AsSelf();
            builder.RegisterType<CashierApp>().AsSelf(); //Register Cashierapp as a dependency
        }
    }
}
