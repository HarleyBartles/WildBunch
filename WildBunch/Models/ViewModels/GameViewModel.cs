using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WildBunch.Shared.Enums;

namespace WildBunch.Models.ViewModels
{
    public class GameViewModel
    {
        public string GameId { get; set; }
        public DifficultyLevel Difficulty { get; set; }
        public string UserId { get; set; }
    }
}
