using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WildBunch.Business.Entities;
using WildBunch.Business.Repositories;
using WildBunch.Data.DbContexts;

namespace WildBunch.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContextFactory<WildBunchContext> _contextFactory;

        public UserRepository(DataContextFactory<WildBunchContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task UpdateAsync(WildBunchUser user)
        {
            using (var context = _contextFactory.Create())
            {
                context.Users.Update(user);
                await context.SaveChangesAsync();
            }
        }
    }
}
