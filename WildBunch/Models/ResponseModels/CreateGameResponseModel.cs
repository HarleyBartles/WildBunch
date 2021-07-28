using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WildBunch.Business.DataTransferObjects;
using WildBunch.Models.ViewModels;
using WildBunch.Shared.Enums;

namespace WildBunch.Models.ResponseModels
{
    public class CreateGameResponseModel
    {
        public string GameId { get; set; }
        public GameViewModel Game { get; set; }
        public CharacterViewModel Character { get; set; }
    }
}
