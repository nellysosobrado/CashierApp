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

        /// <summary>
        /// Constructor initializes the service for handling receipt files
        /// </summary>
        public ReceiptFactory()
        {
            _receiptFileService = new ReceiptFileService();
        }

        /// <summary>
        /// Creates a new receipt with a unique receipt number, including the specified cart items and total price.
        /// </summary>
        /// <param name="cart">A list of tuples where each tuple contains a product and its corresponding quantity.</param>
        /// <param name="totalPrice">The total price of all items in the cart.</param>
        /// <returns>A new <see cref="Receipt"/> object initialized with the provided cart items and total price.</returns>
        public Receipt CreateReceipt(List<(IProducts Product, int Quantity)> cart, decimal totalPrice)
        {
            string filePath = _receiptFileService.GetFilePath();

            int receiptNumber = _receiptFileService.GetLastReceiptNumber(filePath);

            return new Receipt(receiptNumber, cart, totalPrice);
        }
    }
}
