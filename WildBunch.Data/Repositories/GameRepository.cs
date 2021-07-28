using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WildBunch.Business.DataTransferObjects;
using WildBunch.Business.Entities;
using WildBunch.Business.Repositories;
using WildBunch.Data.DbContexts;

namespace WildBunch.Data.Repositories
{
    public class GameRepository : IGameRepository
    {
        private readonly DataContextFactory<WildBunchContext> _contextFactory;

        public GameRepository(DataContextFactory<WildBunchContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<string> InsertAsync(Game game)
        {
            using (var context = _contextFactory.Create())
            {
                await context.Games.AddAsync(game);

                await context.SaveChangesAsync();

                return game.GameId;
            }
        }

        public async Task<Game> FindAsync(string gameId)
        {
            using (var context = _contextFactory.Create())
            {
                return await context.Games
                    .FirstOrDefaultAsync(g => g.GameId == gameId);
            }
        }

        public async Task UpdateAsync(Game game)
        {
            using (var context = _contextFactory.Create())
            {
                context.Games.Update(game);
                await context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Game>> GetGamesAsync(Expression<Func<Game, bool>> filter = null, bool asNoTracking = false)
        {
            using (var context = _contextFactory.Create())
            {
                var query = context.Games
                    .Where(filter);

                if (asNoTracking)
                {
                    return await query
                        .AsNoTracking()
                        .ToListAsync();
                }

                return await query
                    .ToListAsync();
            }
        }

        public async Task<Game> GetAsync(Expression<Func<Game, bool>> filter = null,            
            Func<IQueryable<Game>, IOrderedQueryable<Game>> orderBy = null,
            bool asNoTracking = false,
            params Expression<Func<Game, object>>[] navigationProperties)
        {
            using (var context = _contextFactory.Create())
            {
                var query = context.Games
                    .Where(filter);

                if (navigationProperties != null)
                    foreach (var property in navigationProperties) { query = query.Include(property); };

                if (orderBy != null)
                    query = orderBy(query);
                
                if (asNoTracking)
                    query = query.AsNoTracking();

                return await query.FirstOrDefaultAsync();
            }
        }
    }
}
