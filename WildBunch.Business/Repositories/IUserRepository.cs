using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WildBunch.Business.Entities;

namespace WildBunch.Business.Repositories
{
    public interface IUserRepository
    {
        Task UpdateAsync(WildBunchUser user);
    }
}
