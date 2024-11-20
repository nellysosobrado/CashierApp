using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CashierApp.Core.Entities;

namespace CashierApp.Core.Interfaces.StoreProducts
{

    public interface IProducts
    {
        int ProductID { get; set; }
        string ProductName { get; set; }
        decimal Price { get; set; }
        string PriceType { get; set; }
        string Category { get; set; }

        List<Campaign> Campaigns { get; set; }
    }
}
