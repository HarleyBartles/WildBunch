using System.Collections.Generic;
using System.Threading.Tasks;
using WildBunch.Business.Entities;
using WildBunch.Business.Repositories;
using WildBunch.Business.DataTransferObjects;
using WildBunch.Shared.Enums;
using System.Linq;
using System.Linq.Expressions;
using System;

namespace WildBunch.Business.Services
{
    public class GameService : IGameService
    {
        private readonly IGameRepository _gameRepository;
        private readonly ICharacterRepository _characterRepository;
        private readonly IInventoryObjectRepository _inventoryObjectRepository;
        private readonly IUserService _userService;

        public GameService(
            IGameRepository gameRepository, 
            ICharacterRepository characterRepository, 
            IInventoryObjectRepository inventoryObjectRepository,
            IUserService userService
            )
        {
            _gameRepository = gameRepository;
            _characterRepository = characterRepository;
            _inventoryObjectRepository = inventoryObjectRepository;
            _userService = userService;
        }

        public async Task<GameDto> CreateGameAsync(string userId, string characterName, DifficultyLevel difficulty)
        {
            var game = new Game 
            {
                Difficulty = difficulty,
                UserId = userId,
                Character = new Character {
                    Name = characterName,
                    Dollars = 50.00,
                    HealthPoints = 1400,
                    Bag = new CharacterBag
                    {
                        Items = new List<BagItem>
                        {
                            new BagItem{ InventoryObjectId = (int)InventoryObjectType.Colt44, QuantityCarried = 1 },
                            new BagItem{ InventoryObjectId = (int)InventoryObjectType.Colt44Bullets, QuantityCarried = 5}
                        }
                    }
                },
                Active = true
            };

            string gameId = await _gameRepository.InsertAsync(game);

            await _userService.SetActiveGameIdAsync(userId, gameId);

            var gamesToInactivate = await _gameRepository.GetGamesAsync(g => g.UserId == userId && g.Active == true && g.GameId != gameId);

            foreach(var g in gamesToInactivate.ToList())
            {
                g.Active = false;
            }

            await _gameRepository.UpdateAsync(game);

            return new GameDto
            {
                GameId = gameId,
                Difficulty = game.Difficulty,
                UserId = game.UserId,
                Character = new CharacterDto(game.Character),
            };

        }

        public async Task<GameDto> GetFullyLoadedGameAsync(string gameId)
        {
            var game = new GameDto( await _gameRepository.GetAsync(g => g.GameId == gameId,
                asNoTracking: true,
                navigationProperties: new Expression<Func<Game, object>>[] { g => g.Character }));

            game.Character = new CharacterDto(await _characterRepository.GetAsync(c => c.CharacterId == game.Character.CharacterId, 
                true,
                new Expression<Func<Character, object>>[] { c => c.Bag }));

            game.Character.Bag = new CharacterBagDto( await _characterRepository.GetCharacterBagAsync(game.Character.Bag.CharacterBagId,
                true,
                new Expression<Func<CharacterBag, object>> [] { b => b.Items }));

            return game;
        }
    }

    public interface IGameService
    {
        Task<GameDto> CreateGameAsync(string userId, string characterName, DifficultyLevel difficulty);
        Task<GameDto> GetFullyLoadedGameAsync(string gameId);
    }
}
