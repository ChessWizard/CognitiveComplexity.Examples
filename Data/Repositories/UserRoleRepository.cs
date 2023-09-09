using Core.Dto;
using Core.Entities;
using Core.Repositories;
using Core.UnitofWork;
using Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class UserRoleRepository : IUserRoleRepository
    {
        private readonly CognitiveComplexityDbContext _context;

        public UserRoleRepository(CognitiveComplexityDbContext context)
        {
            _context = context;
        }

        public async Task AddRoleToUserAsync(List<RoleDto> roles, User user)
        {
            foreach (var role in roles)
            {
                var addToRole = await _context.Roles.FirstAsync(x => x.RoleType == role.RoleType);
                UserRole userRole = new()
                {
                    Role = addToRole,
                    User = user
                };
                await _context.UserRoles.AddAsync(userRole);
            }
        }
    }
}
