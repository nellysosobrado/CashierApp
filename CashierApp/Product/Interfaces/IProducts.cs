using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashierApp.Product.Interfaces
{

    public interface IProducts
    {
        int ProductID { get; set; }
        string ProductName { get; set; }
        decimal Price { get; set; }
        string PriceType { get; set; }
        string Category { get; set; }

        // Endast referens till kampanjer
        List<Campaign> Campaigns { get; set; }
    }
}
