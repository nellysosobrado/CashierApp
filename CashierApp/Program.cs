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
            //// Skapa en lista med testprodukter
            //List<IProducts> testProducts = new List<IProducts>
            //{
            //    ProductFactory.CreateProduct("fruit", 101, "Apple", 1.99m, "piece"),
            //    ProductFactory.CreateProduct("meat", 102, "Chicken", 5.99m, "kg"),
            //    ProductFactory.CreateProduct("drink", 103, "Milk", 1.49m, "liter"),
            //    ProductFactory.CreateProduct("bakery", 104, "Bread", 2.49m, "piece"),
            //    ProductFactory.CreateProduct("dairy", 105, "Cheese", 3.99m, "kg")
            //};

            //// Skriv ut egenskaperna för varje produkt
            //foreach (var product in testProducts)
            //{
            //    Console.WriteLine("Product created:");
            //    Console.WriteLine($" - Product ID: {product.ProductID}");
            //    Console.WriteLine($" - Name      : {product.Name}");
            //    Console.WriteLine($" - Category  : {product.Category}");
            //    Console.WriteLine($" - Price     : {product.Price:C}");
            //    Console.WriteLine($" - PriceType : {product.PriceType}");
            //    Console.WriteLine();
            //}

            //Console.WriteLine("Test complete. Press any key to exit.");
            //Console.ReadKey();


            var builder = new ContainerBuilder();//Container in order to manage the dependencies
            DependencyRegister.RegisterDependencies(builder); //Register dependencies


            using (var container = builder.Build())
            {
                var app = container.Resolve<CashierApp>(); // Creates instances of all the dependencies
                app.Run();
            }
        }
    }
}
