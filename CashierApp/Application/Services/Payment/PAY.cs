using CashierApp.Core.Entities;
using CashierApp.Application.Factories;
using CashierApp.Application.Services.StoreReceipts;
using CashierApp.Application.Services.Campaigns;
using CashierApp.Core.Interfaces.StoreProducts;


namespace CashierApp.Application.Services.Payment
{
    /// <summary>
    /// PaymentService, takes care of the payment. And calls receipt class
    /// </summary>
    public class PAY
    {
        private readonly ReceiptFactory _receiptFactory;
        private readonly ReceiptService _receiptService;

        public PAY(CampaignService campaignService)
        {
            _receiptFactory = new ReceiptFactory();
            _receiptService = new ReceiptService(campaignService); // Skicka CampaignService här
        }

        public void CreateReceipt(List<(IProducts Product, int Quantity)> cart, decimal totalPrice)
        {
            Console.WriteLine("Processing payment and generating receipt...");

            Receipt receipt = _receiptFactory.CreateReceipt(cart, totalPrice);
            string receiptContent = _receiptService.CreateReceipt(receipt);
            string filePath = _receiptService.GetFilePath();

            _receiptService.SaveReceiptToFile(receiptContent, filePath);
            ReceiptService.ReceiptDisplay(receiptContent);
        }
    }
}
