﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CashierApp.Core.Entities;

namespace CashierApp.Core.Interfaces
{
    public interface IProductService
    {
        IProducts GetProductById(int productId);
        IProducts GetProductByName(string productName);

        void AddCampaignToProduct(int productId, Campaign campaign);
        void UpdateProduct(IProducts product);

        void AddProduct(string category, int productId, string productName, decimal price, string priceType);
        void UpdateProductName(int productId, string newName);
        void UpdateProductPrice(int productId, decimal newPrice);

        void RemoveProduct(int productId);

        List<IProducts> GetAllProducts();


    }
}
