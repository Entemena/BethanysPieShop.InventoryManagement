﻿using BethanysPieShop.InventoryManagement.Domain.Contracts;
using BethanysPieShop.InventoryManagement.Domain.General;
using System.Runtime.Serialization;

namespace BethanysPieShop.InventoryManagement.Domain.ProductManagement
{
    public class BulkProduct : Product, ISaveable
    {
        public BulkProduct(int id, string name, string? description, Price price, int maxAmountInStock) : base(id, name, description, price, UnitType.PerKG, maxAmountInStock)
        {
        }

        public string ConvertToStringForSaving()
        {
            return $"{Id};{Name};{Description};{maxItemsInStock};{Price.ItemPrice};{(int)Price.Currency};{(int)UnitType};3;";
        }

        public override void IncreaseStock()
        {
            AmountInStock++;
        }

    }
}