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
    //PaymentSAerivce - Logic and call the specific classes/methods needed to example: create a receipt
    //ReceiptCreater
    //ReceiptManager
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
            string receiptContent = CreateReceipt(receipt);
            string filePath = _receiptManager.GetFilePath();



            _receiptManager.SaveReceiptToFile(receiptContent, filePath);

            ReceiptDisplay(receiptContent); //Display receipt onto console

            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
        }

        private string CreateReceipt(Receipt receipt)
        {
            var receiptBuilder = new System.Text.StringBuilder();
            receiptBuilder.AppendLine("\n==== Store ==============");
            receiptBuilder.AppendLine("Store Liljeholmen, Stockholm");
            receiptBuilder.AppendLine("Årstaängsvägen 31");
            receiptBuilder.AppendLine("117 43 Stockholm");
            receiptBuilder.AppendLine("Org:123456-1234 ");
            receiptBuilder.AppendLine("--------------------------");
            receiptBuilder.AppendLine($"Receipt number: {receipt.ReceiptNumber}");
            receiptBuilder.AppendLine("Staff: Name \t Trans: 123456");
            receiptBuilder.AppendLine($"Date: {receipt.Date}");
            receiptBuilder.AppendLine("--------------------------");
            receiptBuilder.AppendLine("Product             Price");

            foreach (var item in receipt.Cart)
            {
                decimal itemTotal = item.Product.Price * item.Quantity;
                receiptBuilder.AppendLine($"{item.Quantity,1}x {item.Product.Name.PadRight(15)} {itemTotal,8:C2}");
            }

            receiptBuilder.AppendLine("...........................\n");
            receiptBuilder.AppendLine($"Total Amount: {receipt.TotalPrice:C2}");
            receiptBuilder.AppendLine("...........................\n");
            receiptBuilder.AppendLine("\nThank you for shopping with us!");
            receiptBuilder.AppendLine("===========================\n");

            return receiptBuilder.ToString();
        }
        public void ReceiptDisplay(string receiptContent)
        {
            Console.Clear();
            Console.WriteLine("\n--- Receipt ---");
            Console.WriteLine(receiptContent);
            Console.WriteLine("\nThank you for shopping with us!");
            Console.WriteLine("===========================");
            Console.WriteLine("Press any key to finish...");
            Console.ReadKey();
        }
    }
}
