﻿using CashierApp.Product.Interfaces;
using CashierApp.Receipts.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashierApp.Receipts.Factories
{
    public class ReceiptFactory
    {
        private readonly ReceiptService _receiptManager;

        public ReceiptFactory()
        {
            _receiptManager = new ReceiptService();
        }

        public Receipt CreateReceipt(List<(IProducts Product, int Quantity)> cart, decimal totalPrice)
        {
            string filePath = _receiptManager.GetFilePath();
            int receiptNumber = _receiptManager.GetLastReceiptNumber(filePath);
            return new Receipt(receiptNumber, cart, totalPrice);
        }
    }
}