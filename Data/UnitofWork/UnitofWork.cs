using Core.UnitofWork;
using Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.UnitofWork
{
    public class UnitofWork : IUnitofWork
    {
        private readonly CognitiveComplexityDbContext _context;
        public UnitofWork(CognitiveComplexityDbContext context)
        {
            _context = context;
        }
        public async Task<int> SaveChangesAsync() 
                            => await _context.SaveChangesAsync();
    }
}
