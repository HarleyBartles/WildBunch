using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WildBunch.Business.DataTransferObjects;
using WildBunch.Business.Entities;
using WildBunch.Business.Repositories;

namespace WildBunch.Business.Services
{
    public class CharacterService : ICharacterService
    {
        private readonly ICharacterRepository _characterRepository;
        private readonly IGameRepository _gameRepository;

        public CharacterService(ICharacterRepository characterRepository, IGameRepository gameRepository)
        {
            _characterRepository = characterRepository;
            _gameRepository = gameRepository;
        }

        public async Task<CharacterDto> GetCharacterByGameAsync(string gameId)
        {
            var game = await _gameRepository.GetAsync(g => g.GameId == gameId,
                new Func<IQueryable<Game>, IOrderedQueryable<Game>>(q => q.OrderBy(game => game.GameId)),
                true,
                new Expression<Func<Game, object>>[] { g => g.Character });

            return new CharacterDto(await _characterRepository.FindAsync(game.Character.CharacterId));
        }
    }

    public interface ICharacterService
    {
        Task<CharacterDto> GetCharacterByGameAsync(string gameId);
    }
}
