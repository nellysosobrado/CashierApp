using CashierApp.Core.Entities;
using CashierApp.Core.Interfaces.Product;
using CashierApp.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashierApp.Application.Factories
{
    public class ReceiptFactory
    {
        private readonly ReceiptFileService _receiptManager;

        public ReceiptFactory()
        {
            _receiptManager = new ReceiptFileService();
        }

        public Receipt CreateReceipt(List<(IProducts Product, int Quantity)> cart, decimal totalPrice)
        {
            string filePath = _receiptManager.GetFilePath();
            int receiptNumber = _receiptManager.GetLastReceiptNumber(filePath);
            return new Receipt(receiptNumber, cart, totalPrice);
        }
    }
}
