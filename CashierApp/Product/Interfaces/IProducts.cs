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

        // Lägg till kampanjegenskaper
        string CampaignDescription { get; set; }
        decimal? CampaignPrice { get; set; }
        DateTime? CampaignStartDate { get; set; }
        DateTime? CampaignEndDate { get; set; }

        // Kontrollera om kampanjen är aktiv
        bool IsCampaignActive();
    }
}
