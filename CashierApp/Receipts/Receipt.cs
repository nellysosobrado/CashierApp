using CashierApp.Product.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashierApp.Receipts
{
    public class Receipt
    {
        //properties
        public int ReceiptNumber { get; }
        public DateTime Date { get; }
        public List<(IProducts Product, int Quantity)> Cart { get; }  
        public decimal TotalPrice { get; }

        public Receipt(int receiptNumber, List<(IProducts Product, int Quantity)> cart, decimal totalPrice)
        {
            ReceiptNumber = receiptNumber;
            Date = DateTime.Now;
            Cart = cart;  
            TotalPrice = totalPrice;
        }


    }
}
