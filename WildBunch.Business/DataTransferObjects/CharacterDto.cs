using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WildBunch.Business.Entities;

namespace WildBunch.Business.DataTransferObjects
{
    public class CharacterDto
    {
        public CharacterDto(Character character)
        {
            CharacterId = character?.CharacterId;
            UserId = character?.UserId;
            Name = character?.Name;
            HealthPoints = character?.HealthPoints?? 0;
            Dollars = character?.Dollars ?? 0;
            Bag = new CharacterBagDto(character?.Bag);
        }

        public string CharacterId { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public int HealthPoints { get; set; }
        public double Dollars { get; set; }
        public CharacterBagDto Bag { get; set; }
    }
}
