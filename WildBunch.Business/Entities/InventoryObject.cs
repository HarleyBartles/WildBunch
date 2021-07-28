using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WildBunch.Business.Entities
{
    public class InventoryObject
    {
        public int InventoryObjectId { get; set; }
        public string Name { get; set; }
        public double BasePrice { get; set; }
        
        public virtual IEnumerable<BagItem> Instances { get; set; }
    }
}
