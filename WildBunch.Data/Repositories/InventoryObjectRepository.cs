using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using WildBunch.Business.Entities;
using WildBunch.Business.Repositories;
using WildBunch.Data.DbContexts;
using WildBunch.Shared.Enums;

namespace WildBunch.Data.Repositories
{
    public class InventoryObjectRepository : IInventoryObjectRepository
    {
        private readonly DataContextFactory<WildBunchContext> _contextFactory;

        public InventoryObjectRepository(DataContextFactory<WildBunchContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }
        public async Task<InventoryObject> FindByNameAsync(string match)
        {
            using (var context = _contextFactory.Create())
            {
                return await context.InventoryObjects.FirstOrDefaultAsync(ob => ob.Name.Contains(match));
            }
        }

        public async Task<InventoryObject> FindByType(InventoryObjectType type)
        {
            using (var context = _contextFactory.Create())
            {
                return await context.InventoryObjects.FindAsync((int)type);
            }
        }
    }
}
