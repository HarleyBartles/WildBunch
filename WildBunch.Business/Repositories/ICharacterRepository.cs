using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WildBunch.Business.Entities;

namespace WildBunch.Business.Repositories
{
    public interface ICharacterRepository
    {
        Task<Character> FindAsync(string characterId);
        Task<Character> GetAsync(Expression<Func<Character, bool>> filter = null, 
            bool asNoTracking = false,
            params Expression<Func<Character, object>>[] navigationProperties);
        Task<CharacterBag> GetCharacterBagAsync(string bagId, bool asNoTracking = false, params Expression<Func<CharacterBag, object>>[] navigationProperties);
    }
}
