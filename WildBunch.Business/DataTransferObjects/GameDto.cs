using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WildBunch.Business.Entities;
using WildBunch.Shared.Enums;

namespace WildBunch.Business.DataTransferObjects
{
    public class GameDto
    {
        public GameDto() { }
        public GameDto(Game game)
        {
            GameId = game?.GameId;
            Difficulty = game?.Difficulty ?? 0;
            UserId = game?.UserId;
            Character = new CharacterDto(game?.Character);
        }
        public string GameId { get; set; }
        public DifficultyLevel Difficulty { get; set; }
        public string UserId { get; set; } // a game belongs to only one user
        public CharacterDto Character { get; set; }
    }
}
