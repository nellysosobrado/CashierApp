using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CashierApp.Application.Services.Campaigns;
using CashierApp.Core.Entities;

namespace CashierApp.Application.Services.StoreReceipts
{



public class ReceiptService
{
    private readonly CampaignService _campaignService;
    private readonly string folderPath = "../../../Receipts/CustomerReceipts";
    private readonly string receiptFileName = $"RECEIPT_{DateTime.Now:yyyyMMdd}.txt";

    public ReceiptService(CampaignService campaignService)
    {
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }
        _campaignService = campaignService;
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
        var receiptBuilder = new StringBuilder();

        // Kvittots header
        receiptBuilder.AppendLine("\n==== Store ==============");
        receiptBuilder.AppendLine("Store Liljeholmen, Stockholm");
        receiptBuilder.AppendLine("Årstaängsvägen 31");
        receiptBuilder.AppendLine("117 43 Stockholm");
        receiptBuilder.AppendLine("Org:123456-1234 ");
        receiptBuilder.AppendLine("--------------------------");
        receiptBuilder.AppendLine($"Receipt number: {receipt.ReceiptNumber}");
        receiptBuilder.AppendLine("Staff: Staff Name \t Trans: 123456");
        receiptBuilder.AppendLine($"Date: {receipt.Date}");
        receiptBuilder.AppendLine("--------------------------");
        receiptBuilder.AppendLine("Product             Price");

        // Grupprodukterna baserat på ProductID
        var groupedCart = receipt.Cart
            .GroupBy(item => item.Product.ProductID)
            .Select(group => new
            {
                Product = group.First().Product,
                Quantity = group.Sum(item => item.Quantity),
                RegularPriceTotal = group.Sum(item => item.Product.Price * item.Quantity),
                Campaign = _campaignService.GetCampaignForProduct(group.Key)
            });

        // Iterera över grupperade produkter
        foreach (var group in groupedCart)
        {
            decimal itemTotal = group.RegularPriceTotal;
            string campaignDescription = string.Empty;

            // Kontrollera om det finns en kampanj
            if (group.Campaign != null && group.Campaign.CampaignPrice.HasValue)
            {
                itemTotal = group.Campaign.CampaignPrice.Value * group.RegularPriceTotal;
                campaignDescription = $"{group.Campaign.Description}               -{group.Campaign.CampaignPrice.Value:C2}";
            }

            // Lägg till produktinformation
            receiptBuilder.AppendLine($"{group.Quantity,2}x {group.Product.ProductName.PadRight(15)} {itemTotal,8:C2}");

            // Lägg till kampanjinformation
            if (!string.IsNullOrWhiteSpace(campaignDescription))
            {
                receiptBuilder.AppendLine($"   {campaignDescription}");
            }
        }

        // Kvittots footer
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
