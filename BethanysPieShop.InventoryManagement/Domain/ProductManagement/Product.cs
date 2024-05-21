﻿using BethanysPieShop.InventoryManagement.Domain.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BethanysPieShop.InventoryManagement.Domain.ProductManagement
{
    public partial class Product
    {
        private int id;
        private string name = string.Empty;
        private string? description;

        private int maxItemsInStock = 0;

        public Product(int Id, string Name)
        {
            this.Id = id;
            this.Name = name;
        }

        public Product(int id) : this(id, string.Empty)
        {
        }

        public Product(int id, string name, string? description, Price price, UnitType unitType, int maxAmountInStock)
        {
            Id = id;
            Name = name;
            Description = description;
            Price = price;
            UnitType = unitType;

            maxItemsInStock = maxAmountInStock;

            UpdateLowStock();
        }

        public int Id
        {
            get { return id; }
            set
            {
                id = value;
            }
        }
        public string Name
        {
            get { return name; }
            set
            {
                name = value.Length > 50 ? value[..50] : value;
            }
        }

        public string Description
        {
            get { return name; }
            set
            {
                if (value == null)
                {
                    description = string.Empty;
                }
                else
                {
                    description = value.Length > 250 ? value[..250] : value;
                }
            }
        }

        public UnitType UnitType { get; set; }

        public int AmountInStock { get; private set; }

        public bool IsBelowStockThreshold { get; private set; }

        public Price Price { get; set; }

        public void UseProduct(int items)
        {
            if (items <= AmountInStock)
            {
                AmountInStock -= items;

                UpdateLowStock();

                Log($"Amount in stock updated. Now {AmountInStock} items in stock.");
            }
            else
            {
                Log($"Not enough items in stock for {CreateSimpleProductRepresentation()}. {AmountInStock} available but {items} requested.");
            }
        }

        private void DecreaseStock(int items, string reason)
        {
            if (items <= AmountInStock)
            {
                AmountInStock -= items;
            }
            else
            {
                AmountInStock = 0;
            }

            UpdateLowStock();

            Log(reason);
        }

        public void IncreaseStock()
        {
            AmountInStock++;

            if (!(AmountInStock <= maxItemsInStock))
            {
                AmountInStock = maxItemsInStock;
                Log($"{CreateSimpleProductRepresentation} stock overflow. 1 item(s) ordered that couldn't be stored.");
            }
            if (AmountInStock > StockThreshold)
            {
                IsBelowStockThreshold = false;
            }
        }
        public void IncreaseStock(int amount)
        {
            int newStock = AmountInStock + amount; 

            if (newStock <= maxItemsInStock)
            {
                AmountInStock += amount;
            }
            else
            {
                AmountInStock = maxItemsInStock;
                Log($"{CreateSimpleProductRepresentation} stock overflow. {newStock - AmountInStock} item(s) ordered that couldn't be stored.");
            }
            if (AmountInStock > StockThreshold)
            {
                IsBelowStockThreshold = false;
            }
        }

        public string DisplayDetailsShort()
        {
            return $"{id}. {name} \n{AmountInStock} items in stock.";
        }

        public string DisplayDetailsFull()
        {
            //StringBuilder sb = new();

            //sb.Append($"{id} {name} \n{description}\n{Price}\n{AmountInStock} items in stock.");

            //if (IsBelowStockThreshold)
            //{
            //    sb.Append("\n !!STOCK LOW!!");
            //}

            //return sb.ToString();
            return DisplayDetailsFull("");
        }

        public string DisplayDetailsFull(string extraDetails)
        {
            StringBuilder sb = new();

            sb.Append($"{id} {name} \n{description}\n{Price}\n{AmountInStock} items in stock.");

            sb.Append(extraDetails);

            if (IsBelowStockThreshold)
            {
                sb.Append("\n !!STOCK LOW!!");
            }

            return sb.ToString();
        }

    }
}