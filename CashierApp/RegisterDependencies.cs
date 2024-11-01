using Autofac;
using CashierApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashierApp
{
    public static class DependencyRegister
    {
        public static void RegisterDependencies(ContainerBuilder builder)
        {
            builder.RegisterType<MenuManager>().AsSelf();
            builder.RegisterType<CustomerManager>().AsSelf();
            builder.RegisterType<AdminManager>().AsSelf();
            builder.RegisterType<ProductService>().AsSelf();
            builder.RegisterType<PaymentService>().AsSelf();
        }
    }
}
