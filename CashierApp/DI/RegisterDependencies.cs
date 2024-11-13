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

            // 1. Register services with no dependencies)
            builder.RegisterAssemblyTypes(assembly)
                   .Where(t => t.Namespace == "CashierApp.ErrorManagement")
                   .AsImplementedInterfaces()
                   .AsSelf()
                   .SingleInstance(); // Example: IErrorManager

            builder.RegisterAssemblyTypes(assembly)
                   .Where(t => t.Namespace != null && t.Namespace.StartsWith("CashierApp.Product"))
                   .AsImplementedInterfaces()
                   .AsSelf()
                   .SingleInstance(); // Example: IProductService

            // 2. Register services that depend on foundational services)
            builder.RegisterType<CustomerInputChecker>().AsSelf().SingleInstance();

            // 3. Register services with higher dependencies
            var namespaces = new[]
            {
                "CashierApp.Customer",
                "CashierApp.Payment",
                "CashierApp.Menu",
                "CashierApp.Admin"
            };

            foreach (var ns in namespaces)
            {
                builder.RegisterAssemblyTypes(assembly)
                       .Where(t => t.Namespace != null && t.Namespace.StartsWith(ns))
                       .AsImplementedInterfaces()
                       .AsSelf();
            }

            builder.RegisterType<MenuService>().As<IMenuService>().SingleInstance();

            // 4. Register the main application program
            builder.RegisterType<CashierSystemApp>().AsSelf();

            // Debug: showcase all dependencies
            builder.RegisterBuildCallback(container =>
            {
                Console.WriteLine("Registered dependencies:");
                foreach (var registration in container.ComponentRegistry.Registrations)
                {
                    Console.WriteLine($"- {registration.Activator.LimitType}");
                }
                Console.ReadLine();
            });
        }
    }
}
