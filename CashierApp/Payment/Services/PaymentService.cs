using CashierApp.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CashierApp.Product.Interfaces;
using CashierApp.Payment;
using CashierApp.Receipts;
using CashierApp.Receipts.Factories;
using CashierApp.Receipts.Services;

namespace CashierApp.Payment.Services
{
    /// <summary>
    /// PaymentService, takes care of the payment. And calls receipt class
    /// </summary>
    public class PaymentService
    {
        private readonly ReceiptFactory _receiptFactory;
        private readonly ReceiptService _receiptManager;

        public PaymentService()
        {
            _receiptFactory = new ReceiptFactory();
            _receiptManager = new ReceiptService();
        }

        public void ProcessPayment(List<(IProducts Product, int Quantity)> cart, decimal totalPrice)
        {
            Console.WriteLine("Processing payment and generating receipt...");

            Receipt receipt = _receiptFactory.CreateReceipt(cart, totalPrice);
            string receiptContent = _receiptManager.CreateReceipt(receipt);
            string filePath = _receiptManager.GetFilePath();

            _receiptManager.SaveReceiptToFile(receiptContent, filePath);
            ReceiptService.ReceiptDisplay(receiptContent);
        }
    }
}
