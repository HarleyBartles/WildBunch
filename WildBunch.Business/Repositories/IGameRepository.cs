using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WildBunch.Business.DataTransferObjects;
using WildBunch.Business.Entities;

namespace WildBunch.Business.Repositories
{
    public interface IGameRepository
    {
        Task<string> InsertAsync(Game game);
        Task<Game> FindAsync(string gameId);
        Task UpdateAsync(Game game);
        Task<IEnumerable<Game>> GetGamesAsync(Expression<Func<Game, bool>> filter = null, bool asNoTracking = false);
        Task<Game> GetAsync(Expression<Func<Game, bool>> filter = null,
            Func<IQueryable<Game>, IOrderedQueryable<Game>> orderBy = null,
            bool asNoTracking = false,
            params Expression<Func<Game, object>>[] navigationProperties);
    }
}
