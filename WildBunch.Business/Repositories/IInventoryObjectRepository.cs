using System.Threading.Tasks;
using WildBunch.Business.Entities;
using WildBunch.Shared.Enums;

namespace WildBunch.Business.Repositories
{
    public interface IInventoryObjectRepository
    {
        Task<InventoryObject> FindByNameAsync(string match);
        Task<InventoryObject> FindByType(InventoryObjectType type);
    }
}
