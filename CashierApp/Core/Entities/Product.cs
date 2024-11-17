using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CashierApp.Core.Interfaces;

namespace CashierApp.Core.Entities
{
    public class Product : IProducts
    {

        public int ProductID { get; set; }
        public required string ProductName { get; set; }
        public decimal Price { get; set; }
        public required string PriceType { get; set; }
        public required string Category { get; set; }



        public List<Campaign> Campaigns { get; set; } = new List<Campaign>();

    }
}
