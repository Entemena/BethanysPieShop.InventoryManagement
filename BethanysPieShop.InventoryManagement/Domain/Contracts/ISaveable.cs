using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BethanysPieShop.InventoryManagement.Domain.Contracts
{
    internal interface ISaveable
    {
        public string ConvertToStringForSaving();
    }
}
