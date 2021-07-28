using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WildBunch.Business.Entities;
using WildBunch.Business.Repositories;
using WildBunch.Business.Services;
using WildBunch.Models;
using WildBunch.Models.RequestModels;
using WildBunch.Models.ResponseModels;
using WildBunch.Models.ViewModels;

namespace WildBunch.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class GameController : ControllerBase
    {
        private readonly ILogger<GameController> _logger;
        private readonly IUserService _userService;
        private readonly ICharacterService _characterService;
        private readonly IGameRepository _gameRepository;
        private readonly IGameService _gameService;

        public GameController(ILogger<GameController> logger, IUserService userService, IGameRepository gameRepository, IGameService gameService, ICharacterService characterService)
        {
            _logger = logger;
            _userService = userService;
            _gameRepository = gameRepository;
            _gameService = gameService;
            _characterService = characterService;
        }

        [HttpGet("get-game")]
        public async Task<ActionResult<UiResponse>> Get(string gameId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var response = new UiResponse();

            try
            {
                var game = await _gameService.GetFullyLoadedGameAsync(gameId);
                var character = game.Character;
                var bag = character.Bag;

                response.Result = new { game, character, bag };
                response.Success = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred in GameController.Get");
                response.Success = false;
                response.Messages = new List<string> { ex.Message };
            }

            return response;
        }

        [HttpPost("create-game")]
        public async Task<ActionResult<UiResponse>> Create([FromBody] CreateGameRequestModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            
            var response = new UiResponse();

            try
            {
                string requestingUserId = await _userService.GetUserIdAsync(User);
                                
                var game = await _gameService.CreateGameAsync(requestingUserId, model.CharacterName, model.Difficulty);

                var character = await _characterService.GetCharacterByGameAsync(game.GameId);

                var result = new CreateGameResponseModel
                {
                    GameId = game.GameId,
                    Game = new GameViewModel
                    {
                        GameId = game.GameId,
                        Difficulty = game.Difficulty,
                        UserId = game.UserId
                    },
                    Character = new CharacterViewModel
                    {
                        CharacterId = character.CharacterId,
                        Name = character.Name,
                        HealthPoints = character.HealthPoints,
                        Dollars = character.Dollars,
                        Bag = new BagViewModel(character.Bag)
                    }
                };

                response.Result = result;
                response.Success = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred in GameController.Create");
                response.Success = false;
                response.Messages = new List<string> { ex.Message };
            }

            return response;
        }
    }
}
