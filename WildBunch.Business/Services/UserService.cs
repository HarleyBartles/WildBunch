using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WildBunch.Business.Entities;
using WildBunch.Business.Repositories;

namespace WildBunch.Business.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<WildBunchUser> _userManager;
        private readonly IGameRepository _gameRepository;
        private readonly IUserRepository _userRepository;

        public UserService(UserManager<WildBunchUser> userManager, IGameRepository gameRepository, IUserRepository userRepository)
        {
            _userManager = userManager;
            _gameRepository = gameRepository;
            _userRepository = userRepository;
        }

        public async Task<string> GetUserIdAsync(ClaimsPrincipal principal)
        {
            var user = await _userManager.GetUserAsync(principal);

            string userId = user?.Id;

            if (string.IsNullOrEmpty(userId))
            {
                userId = principal.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;
            }

            return userId;
        }

        public async Task<string> GetActiveGameIdAsync(string userId)
        {
            var activeGame = await _gameRepository.GetAsync(game => game.UserId == userId && game.Active == true,
                asNoTracking: true);

            return activeGame?.GameId;
        }

        public async Task SetActiveGameIdAsync(string userId, string gameId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            user.ActiveGameId = gameId;

            await _userRepository.UpdateAsync(user);
        }
    }

    public interface IUserService
    {
        Task<string> GetUserIdAsync(ClaimsPrincipal principal);
        Task<string> GetActiveGameIdAsync(string userId);
        Task SetActiveGameIdAsync(string userId, string gameId);
    }
}
