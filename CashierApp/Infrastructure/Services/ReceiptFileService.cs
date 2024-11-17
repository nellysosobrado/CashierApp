using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashierApp.Infrastructure.Services
{
    public class ReceiptFileService
    {
        private readonly string folderPath = "../../../Receipts/CustomerReceipts";
        private readonly string receiptFileName = $"RECEIPT_{DateTime.Now:yyyyMMdd}.txt";

        public ReceiptFileService()
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

        public int GetLastReceiptNumber(string filePath)
        {
            if (!File.Exists(filePath)) return 1;

            var lines = File.ReadAllLines(filePath);
            int lastReceiptNumber = 0;

            foreach (var line in lines)
            {
                if (line.StartsWith("Receipt number:"))
                {
                    var parts = line.Split(new char[] { '#', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    if (parts.Length > 2 && int.TryParse(parts[2], out int number))
                    {
                        lastReceiptNumber = Math.Max(lastReceiptNumber, number);
                    }
                }
            }

            return lastReceiptNumber + 1;
        }
    }
}
