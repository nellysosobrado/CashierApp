using Autofac;
using CashierApp.DI;
using CashierApp.Product.Factories;
using CashierApp.Product.Interfaces;

namespace CashierApp
{
    internal class Program
    {
        static void Main(string[] args)
        {

            var builder = new ContainerBuilder();//Container in order to manage the dependencies
            DependencyRegister.RegisterDependencies(builder); //Register dependencies


            using (var container = builder.Build())
            {
                var application = container.Resolve<CashierSystemApp>(); // Creates instances of all the dependencies
                application.RunApp();
            }
        }
    }
}
