using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WildBunch.Business.Entities;
using WildBunch.Business.Repositories;
using WildBunch.Data.DbContexts;

namespace WildBunch.Data.Repositories
{
    public class CharacterRepository : ICharacterRepository
    {
        private readonly DataContextFactory<WildBunchContext> _contextFactory;

        public CharacterRepository(DataContextFactory<WildBunchContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }
        public async Task<Character> FindAsync(string characterId)
        {
            using (var context = _contextFactory.Create())
            {
                return await context.Characters
                    .Include(c => c.Bag)
                    .ThenInclude(c => c.Items)
                    .FirstOrDefaultAsync(c => c.CharacterId == characterId);
            }
        }

        public async Task<Character> GetAsync(Expression<Func<Character, bool>> filter = null, bool asNoTracking = false, params Expression<Func<Character, object>>[] navigationProperties )
        {
            using (var context = _contextFactory.Create())
            {
                var query = context.Characters
                    .Where(filter);

                if (navigationProperties != null)
                    foreach (var property in navigationProperties) { query = query.Include(property); };

                if (asNoTracking)
                    query = query.AsNoTracking();
                
                return await query.FirstOrDefaultAsync();
            }
        }

        public async Task<CharacterBag> GetCharacterBagAsync(string bagId, bool asNoTracking = false, params Expression<Func<CharacterBag, object>>[] navigationProperties )
        {
            using (var context = _contextFactory.Create())
            {
                var query = context.CharacterBags
                    .Where(b => b.CharacterBagId == bagId);

                if (navigationProperties != null)
                    foreach (var property in navigationProperties) { query = query.Include(property); };

                if (asNoTracking)
                    query = query.AsNoTracking();
                
                return await query.FirstOrDefaultAsync();
            }
        }
    }
}
