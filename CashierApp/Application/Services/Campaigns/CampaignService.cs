using CashierApp.Core.Entities;
using CashierApp.Core.Interfaces.ErrorManagement;
using CashierApp.Core.Interfaces.StoreCampaigns;
using CashierApp.Core.Interfaces.StoreProducts;




namespace CashierApp.Application.Services.Campaigns
{
    public class CampaignService : ICampaignManager
    {
        private readonly IProductService _productService;
        private readonly IErrorManager _errorManager;

        public CampaignService(IProductService productService, IErrorManager errorManager)
        {
            _productService = productService;
            _errorManager = errorManager;
        }

        //ADD CAMPAIGN
        public void AddCampaign()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("ADD CAMPAIGN");

                // Hämta Produkt-ID
                Console.Write("Enter the Product ID for the campaign: ");
                if (!int.TryParse(Console.ReadLine(), out int productId))
                {
                    _errorManager?.DisplayError("Invalid Product ID. Please enter a valid numberPress any key to try again.");
                    continue; // Återgå till början av loopen
                }

                var product = _productService.GetProductId(productId);
                if (product == null)
                {
                    _errorManager?.DisplayError("Product not found. Please enter a valid Product ID.Press any key to try again");
                    Console.ReadKey();
                    continue; // Återgå till början av loopen
                }

                // Hämta Kampanjpris
                Console.Write("Enter the campaign price: ");
                if (!decimal.TryParse(Console.ReadLine(), out decimal campaignPrice) || campaignPrice <= 0)
                {
                    _errorManager?.DisplayError("Invalid price. Please enter a valid positive number. Press any key to try again");
                    Console.ReadKey();
                    continue;
                }

                // Hämta Startdatum
                Console.Write("Enter the start date (yyyy-MM-dd): ");
                if (!DateTime.TryParse(Console.ReadLine(), out DateTime startDate))
                {
                    _errorManager?.DisplayError("Invalid date format. Please enter a valid date (yyyy-MM-dd).Press any key to try again");
                    Console.ReadKey();
                    continue;
                }

                // Hämta Slutdatum
                Console.Write("Enter the end date (yyyy-MM-dd): ");
                if (!DateTime.TryParse(Console.ReadLine(), out DateTime endDate) || endDate < startDate)
                {
                    _errorManager?.DisplayError("Invalid end date. The end date must be later than or equal to the start date.Press any key to try again");
                    Console.ReadKey();
                    continue;
                }

                // Hämta Beskrivning
                Console.Write("Enter the campaign description: ");
                string description = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(description))
                {
                    _errorManager?.DisplayError("Description cannot be empty. Please enter a valid description.Press any key to try again");
                    Console.ReadKey();
                    continue;
                }

                // Skapa Kampanjen
                var campaign = new Campaign
                {
                    Description = description,
                    CampaignPrice = campaignPrice,
                    StartDate = startDate,
                    EndDate = endDate
                };

                _productService.AddCampaignToProduct(productId, campaign);
                Console.WriteLine("Campaign added successfully!");
                Console.ReadKey();
                return; // Avsluta funktionen när processen är klar
            }
        }



        //REMOVE CAMPAIGN
        public void RemoveCampaign()
        {
            Console.Clear();
            Console.WriteLine("REMOVE CAMPAIGN");

            Console.Write("Enter the Product ID for the campaign to remove: ");
            if (!int.TryParse(Console.ReadLine(), out int productId))
            {
                Console.WriteLine("Invalid Product ID. Please enter a valid number.");
                Console.ReadKey();
                return;
            }

            var product = _productService.GetProductId(productId);
            if (product == null)
            {
                Console.WriteLine("Product not found.");
                Console.ReadKey();
                return;
            }

            if (product.Campaigns == null || !product.Campaigns.Any())
            {
                Console.WriteLine("No campaigns found for this product.");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("Available campaigns:");
            for (int i = 0; i < product.Campaigns.Count; i++)
            {
                var campaign = product.Campaigns[i];
                Console.WriteLine($"{i + 1}. {campaign.Description} (Price: {campaign.CampaignPrice}, {campaign.StartDate:yyyy-MM-dd} to {campaign.EndDate:yyyy-MM-dd})");
            }

            Console.Write("Select the campaign number to remove: ");
            if (!int.TryParse(Console.ReadLine(), out int campaignIndex) || campaignIndex < 1 || campaignIndex > product.Campaigns.Count)
            {
                Console.WriteLine("Invalid selection.");
                Console.ReadKey();
                return;
            }

            product.Campaigns.RemoveAt(campaignIndex - 1);
            _productService.UpdateProduct(product);

            Console.WriteLine("Campaign removed successfully.");
            Console.ReadKey();
        }
        public bool IsCampaignActive(Campaign campaign)
        {
            if (campaign == null)
            {
                throw new ArgumentNullException(nameof(campaign), "Campaign cannot be null.");
            }

            var currentDate = DateTime.Now;
            return currentDate >= campaign.StartDate && currentDate <= campaign.EndDate;
        }

        public List<Campaign> GetActiveCampaigns(List<Campaign> campaigns)
        {
            if (campaigns == null || !campaigns.Any())
            {
                return new List<Campaign>(); 
            }

            var currentDate = DateTime.Now;
            return campaigns.Where(c => c.StartDate <= currentDate && c.EndDate >= currentDate).ToList();
        }
        public Campaign? GetCampaignForProduct(int productId)
        {
            var product = _productService.GetProductId(productId);
            if (product == null || product.Campaigns == null || !product.Campaigns.Any())
            {
                return null; 
            }
            return product.Campaigns.FirstOrDefault(IsCampaignActive);
        }
    }
}
