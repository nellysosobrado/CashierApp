using Autofac;

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
                var app = container.Resolve<CashierApp>(); // Create an instance of CashierApp
                app.Run(); // Run the application
            }
        }
    }
}
