using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CashierApp.Core.Interfaces;
using CashierApp.Core.Entities;
using CashierApp.Application.Factories;

//using CashierApp.Product;

namespace CashierApp.Application.Services
{
    /// <summary>
    /// PaymentService, takes care of the payment. And calls receipt class
    /// </summary>
    public class PAY
    {
        private readonly ReceiptFactory _receiptFactory;
        private readonly ReceiptService _receiptManager;

        public PAY()
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
