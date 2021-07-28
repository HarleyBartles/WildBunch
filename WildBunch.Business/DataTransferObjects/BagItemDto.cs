using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WildBunch.Business.Entities;
using WildBunch.Shared.Enums;

namespace WildBunch.Business.DataTransferObjects
{
    public class BagItemDto
    {
        public BagItemDto(BagItem item)
        {
            InventoryObjectId = item?.InventoryObjectId ?? 0;
            QuantityCarried = item?.QuantityCarried ?? 0;
        }

        public int InventoryObjectId { get; set; }
        public int QuantityCarried { get; set; }
        public InventoryObjectType Type
        {
            get => (InventoryObjectType)InventoryObjectId;
            set => InventoryObjectId = (int)value;
        }
    }
}
