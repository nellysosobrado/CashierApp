using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashierApp.Receipts.Services
{
    public class ReceiptService
    {
        private readonly string folderPath = "../../../Receipts/CustomerReceipts";
        private readonly string receiptFileName = $"RECEIPT_{DateTime.Now:yyyyMMdd}.txt";

        public ReceiptService()
        {
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
        }

        public string GetFilePath()
        {
            return Path.Combine(folderPath, receiptFileName);
        }

        public void SaveReceiptToFile(string receiptContent, string filePath)
        {
            using (StreamWriter writer = new StreamWriter(filePath, append: true))
            {
                writer.WriteLine(receiptContent);
            }
            Console.WriteLine($"Receipt saved to {filePath}");
        }

        public string CreateReceipt(Receipt receipt)
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

        public static void ReceiptDisplay(string receiptContent)
        {
            Console.Clear();
            Console.WriteLine(receiptContent);
            Console.WriteLine("Press any key to go back to main menu..");
            Console.ReadKey();
        }
    }
}
