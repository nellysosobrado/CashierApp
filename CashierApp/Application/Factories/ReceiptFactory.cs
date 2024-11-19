using CashierApp.Core.Entities;
using CashierApp.Core.Interfaces.StoreProducts;
using CashierApp.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashierApp.Application.Factories
{
    /// <summary>
    /// This class creates receipts and handles their setup
    /// </summary>
    public class ReceiptFactory
    {
        private readonly ReceiptFileService _receiptFileService;
        public ReceiptFactory()
        {
            _receiptFileService = new ReceiptFileService();
        }

        /// <summary>
        /// Creates a new receipt with a unique receipt number, including the specified cart items and total price.
        /// </summary>
        public Receipt CreateReceipt(List<(IProducts Product, int Quantity)> cart, decimal totalPrice)
        {
            string filePath = _receiptFileService.GetFilePath();

            int receiptNumber = _receiptFileService.GetLastReceiptNumber(filePath);

            return new Receipt(receiptNumber, cart, totalPrice);
        }
    }
}
