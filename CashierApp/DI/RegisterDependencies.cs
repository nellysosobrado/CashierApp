using Autofac;
using CashierApp.Admin;
using CashierApp.Customer;
using CashierApp.ErrorManagement;
using CashierApp.Menu;
using CashierApp.Menu.Interface;
using CashierApp.Payment;
using CashierApp.Product;
using CashierApp.Product.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CashierApp.DI
{
    public static class DependencyRegister
    {
        public static void RegisterDependencies(ContainerBuilder builder)
        {
            // Get the assembly containing all types in the current project
            var assembly = Assembly.GetExecutingAssembly();

            
            builder.RegisterAssemblyTypes(assembly)
                   .Where(t => t.Namespace == "CashierApp.ErrorManagement")
                   .AsImplementedInterfaces()
                   .AsSelf()
                   .SingleInstance(); 

            // List of namespaces for registering related types to reduce duplicate code
            var namespaces = new[]
            {
                "CashierApp.Product",
                "CashierApp.Product.Services",
                "CashierApp.Customer",
                "CashierApp.Payment",
                "CashierApp.Menu",
                "CashierApp.Admin"
            };

            // Register all types based on the namespaces list
            foreach (var ns in namespaces)
            {
                builder.RegisterAssemblyTypes(assembly)
                       .Where(t => t.Namespace == ns)
                       .AsSelf(); 
            }

            builder.RegisterType<MenuService>().As<IMenuService>().SingleInstance();

            // Register the main application program
            builder.RegisterType<CashierSystemApp>().AsSelf();
        }
    }
}
