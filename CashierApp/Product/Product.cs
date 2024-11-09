using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CashierApp.Product.Interfaces;

namespace CashierApp.Product
{
    public class Product : IProducts
    {
        //Properties
        public int ProductID { get; set; }
        public required string ProductName { get; set; }
        public decimal Price { get; set; }
        public required string PriceType { get; set; }
        public int Quantity { get; set; }
        public required string Category { get; set; }
    }
}
