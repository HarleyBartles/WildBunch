using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WildBunch.Business.Entities
{
    public class Character
    {
        public string CharacterId { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public int HealthPoints { get; set; }
        public double Dollars { get; set; }
        
        public virtual CharacterBag Bag { get; set; }
        public virtual WildBunchUser User { get; set; }
    }
}
