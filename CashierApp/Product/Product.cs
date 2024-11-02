using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashierApp.Product
{
    public class Product
    {
        //properties, getters
        public int ProductID { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string PriceType { get; set; }

        public Product(int productId, string name, decimal price, string priceType)
        {
            ProductID = productId;
            Name = name;
            Price = price;
            PriceType = priceType;
        }


    }
}
