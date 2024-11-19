using CashierApp.Core.Entities;
using CashierApp.Core.Interfaces.StoreCampaigns;
using CashierApp.Core.Interfaces.StoreProducts;




namespace CashierApp.Application.Services.Campaigns
{
    public class CampaignService : ICampaignManager
    {
        private readonly IProductService _productService;

        public CampaignService(IProductService productService)
        {
            _productService = productService;
        }

        //ADD CAMPAIGN
        public void AddCampaign()
        {
            Console.Clear();
            Console.WriteLine("ADD CAMPAIGN");

            Console.Write("Enter the Product ID for the campaign: ");
            if (!int.TryParse(Console.ReadLine(), out int productId))
            {
                Console.WriteLine("Invalid Product ID. Please enter a valid number.");
                return;
            }

            var product = _productService.GetProductById(productId);
            if (product == null)
            {
                Console.WriteLine("Product not found.");
                Console.ReadKey();
                return;
            }

            Console.Write("Enter the campaign price: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal campaignPrice) || campaignPrice <= 0)
            {
                Console.WriteLine("Invalid price. Please enter a valid positive number.");
                return;
            }

            Console.Write("Enter the start date (yyyy-MM-dd): ");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime startDate))
            {
                Console.WriteLine("Invalid date format.");
                return;
            }

            Console.Write("Enter the end date (yyyy-MM-dd): ");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime endDate) || endDate < startDate)
            {
                Console.WriteLine("Invalid end date.");
                return;
            }

            Console.Write("Enter the campaign description: ");
            string description = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(description))
            {
                Console.WriteLine("Description cannot be empty.");
                return;
            }

            var campaign = new Campaign
            {
                Description = description,
                CampaignPrice = campaignPrice,
                StartDate = startDate,
                EndDate = endDate
            };

            _productService.AddCampaignToProduct(productId, campaign);
            Console.WriteLine($"Campaign successfully added to product ID {productId}.");
            Console.ReadKey();
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

            var product = _productService.GetProductById(productId);
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
            var product = _productService.GetProductById(productId);
            if (product == null || product.Campaigns == null || !product.Campaigns.Any())
            {
                return null; 
            }
            return product.Campaigns.FirstOrDefault(IsCampaignActive);
        }
    }
}
