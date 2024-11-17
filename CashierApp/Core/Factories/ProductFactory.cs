using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CashierApp.Core.Entities; 
//using CashierApp.Product.Interfaces; 


namespace CashierApp.Core.Factories
{
    public static class ProductFactory
    {
        public static Product CreateProduct(string category, int productId, string productName, decimal price, string priceType)
        {
            return new Product
            {
                ProductID = productId,
                ProductName = productName,
                Category = category,
                Price = price,
                PriceType = priceType,
                Campaigns = new List<Campaign>()
            };
        }
    }
}
