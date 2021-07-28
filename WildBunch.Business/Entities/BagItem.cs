using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WildBunch.Business.Entities
{
    public class BagItem
    {
        public string BagItemId { get; set; }
        public string CharacterBagId { get; set; }
        public int InventoryObjectId { get; set; }
        public int QuantityCarried { get; set; }

        public virtual InventoryObject InventoryObject { get; set; }
        public virtual CharacterBag Bag { get; set; }
    }
}
