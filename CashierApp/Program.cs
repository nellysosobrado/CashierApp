using Autofac;
using CashierApp.Infrastructure.DI;

namespace CashierApp
{
    internal class Program
    {
        /// <summary>
        /// Program.cs creates and configures a container with the help of Autofac to manage all dependencies.
        /// It acts as the starting point where all dependencies are set up and the main program is started through RunApp.
        /// </summary>
        static void Main(string[] args)
        {
            var builder = new ContainerBuilder();
           
            //'Registerdependencies' register all the app dependencies to builder
            DependencyRegister.RegisterDependencies(builder); 

            //Builds the container, to resolve dependencies and manage their lifetimes
            using (var container = builder.Build())
            {
                var application = container.Resolve<CashierSystemApp>(); 
                application.RunApp();//Runs the cashier application
            }
        }
    }
}
