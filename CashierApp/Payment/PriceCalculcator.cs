using CashierApp.Product.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashierApp.Payment
{
    public static class PriceCalculator
    {
        public static decimal CalculateTotalPrice(List<(IProducts Product, int Quantity)> cart)
        {
            decimal total = 0;
            foreach (var item in cart)
            {
                total += item.Product.Price * item.Quantity;
            }
            return total;
        }
    }
}
