using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WildBunch.Business.Entities;
using WildBunch.Shared.Enums;

namespace WildBunch.Models.RequestModels
{
    public class CreateGameRequestModel
    {
        public string CharacterName { get; set; }
        public DifficultyLevel Difficulty { get; set; }
    }
}
