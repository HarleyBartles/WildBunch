using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WildBunch.Shared.Enums;

namespace WildBunch.Business.Entities
{
    public class Game
    {
        public string GameId { get; set; }
        public DifficultyLevel Difficulty { get; set; }
        public string CharacterId { get; set; }
        public string UserId { get; set; } // a game belongs to only one user
        public bool Active { get; set; }
        public int? TownId { get; set; }

        public virtual Town CurrentTown { get; set; }
        public virtual WildBunchUser User { get; set; }
        public virtual Character Character { get; set; }
    }
}
