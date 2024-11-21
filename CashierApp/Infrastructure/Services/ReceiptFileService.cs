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

        /// <summary>
        /// //Constructor: Ensures the receipt folder exists.
        /// </summary>
        public ReceiptFileService() 
        {
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
        }

        /// <summary>
        /// Gets the full path of the receipt file.
        /// </summary>
        /// <returns></returns>
        public string GetFilePath()
        {
            return Path.Combine(folderPath, receiptFileName);
        }

        /// <summary>
        /// Saves the receipt content to the specified file.
        /// </summary>
        /// <param name="receiptContent">The content of the receipt to save.</param>
        /// <param name="filePath">The file path where the receipt should be saved.</param>
        public void SaveReceiptToFile(string receiptContent, string filePath)
        {
            using (StreamWriter writer = new StreamWriter(filePath, append: true))
            {
                writer.WriteLine(receiptContent);
            }
            Console.WriteLine($"Receipt saved to {filePath}");
        }

        /// <summary>
        /// Gets the last receipt number from the receipt file.
        /// </summary>
        /// <param name="filePath">The file path of the receipt file to read.</param>
        /// <returns>The next receipt number to use.</returns>
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
