using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.UnitofWork
{
    public interface IUnitofWork
    {
        Task<int> SaveChangesAsync();
    }
}
