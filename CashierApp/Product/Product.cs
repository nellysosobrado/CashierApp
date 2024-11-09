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
        public int ProductID { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string PriceType { get; set; }
        public int Quantity { get; set; }
        public string? Category { get; set; }
    }
}
