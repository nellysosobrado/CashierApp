using Autofac;
using CashierApp.DI;

namespace CashierApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var builder = new ContainerBuilder();//Container in order to manage the dependencies
            DependencyRegister.RegisterDependencies(builder); //Register dependencies
            builder.RegisterType<CashierApp>().AsSelf(); //Register Cashierapp as a dependency

            using (var container = builder.Build())
            {
                var app = container.Resolve<CashierApp>(); // Creates instances of all the dependencies
                app.Run(); // Run the application
            }
        }
    }
}
