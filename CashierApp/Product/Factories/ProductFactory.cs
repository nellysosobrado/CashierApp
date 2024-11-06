using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CashierApp.Product.Interfaces;

namespace CashierApp.Product.Factories
{
    public static class ProductFactory
    {
        public static IProducts CreateProduct(string category, int productId, string name, decimal price, string priceType)
        {
            //Console.WriteLine($"Creating product: {name}, Category: {category}, Price Type: {priceType}");


            var product = new Product
            {
                ProductID = productId,
                Name = name,
                Price = price,
                PriceType = priceType,
                Category = category 
            };

            return product;

        }
    }
}
