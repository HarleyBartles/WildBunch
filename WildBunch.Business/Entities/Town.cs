using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WildBunch.Shared.Enums;

namespace WildBunch.Business.Entities
{
    public class Town
    {
        public int TownId { get; set; }
        public string Name { get; set; }
        public TownEnum TownType => (TownEnum)TownId;
        public StrangerEnum Stranger { get; set; }
    }

    
}
