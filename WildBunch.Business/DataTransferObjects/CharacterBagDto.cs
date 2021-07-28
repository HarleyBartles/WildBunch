using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WildBunch.Business.Entities;

namespace WildBunch.Business.DataTransferObjects
{
    public class CharacterBagDto
    {
        public CharacterBagDto(CharacterBag bag)
        {
            CharacterBagId = bag?.CharacterBagId;
            Items = bag?.Items?.Select(item => new BagItemDto(item));
        }

        public string CharacterBagId { get; set; }
        public IEnumerable<BagItemDto> Items { get; set; }
    }
}
