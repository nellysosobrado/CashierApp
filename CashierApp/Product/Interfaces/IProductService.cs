using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashierApp.Product.Interfaces
{
     public interface IProductService
    {
        IProducts GetProductById(int productId);
        IProducts GetProductByName(string productName);

    }
}
