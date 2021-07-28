using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WildBunch.Business.Entities
{
    public class CharacterBag
    {
        public string CharacterBagId { get; set; }
        public string CharacterId { get; set; }
        public IEnumerable<BagItem> Items { get; set; }

        public virtual Character Character { get; set; }
    }
}
