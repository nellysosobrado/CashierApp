using Autofac;
using CashierApp.DI;
using CashierApp.Product.Factories;
using CashierApp.Product.Interfaces;

namespace CashierApp
{
    internal class Program
    {
        /// <summary>
        /// Program.cs creates and configures a container with the help of Autofac to manage all dependencies.
        /// It acts as the starting point where all dependencies are set up and the main program is started through RunApp.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            //ContainerBuilder, handles all the dependencies in the application. 
            var builder = new ContainerBuilder();
            DependencyRegister.RegisterDependencies(builder); //Register dependencies with builder


            using (var container = builder.Build())
            {
                var application = container.Resolve<CashierSystemApp>(); // Creates instances of all the dependencies
                application.RunApp();
            }
        }
    }
}
