using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashierApp.Core.Interfaces.StoreProducts
{
    public interface IProductManager
    {
        void CreateNewProduct();
        void EditProductDetails();
        void RemoveProduct();
        void DisplayProductsAndCampaigns();
    }
}
