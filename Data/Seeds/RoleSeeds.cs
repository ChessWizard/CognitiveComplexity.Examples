using Core.Entities;
using Core.Enums;
using Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Seeds
{
    public class RoleSeeds
    {
        public static async Task AddSeedRoles(CognitiveComplexityDbContext context)
        {
            var roles = await context.Roles
                .AsNoTracking()
                .ToListAsync();

            if (!roles.Any() && roles is not null)
            {
                List<Role> addingRoles = new()
                {
                    new() { RoleType = RoleType.Admin, Title = "Admin Rolü"},
                    new() { RoleType = RoleType.Corporate, Title = "Kurumsal Kullanıcı Rolü"},
                    new() { RoleType = RoleType.Individual, Title = "Bireysel Kullanıcı Rolü"},
                    new() { RoleType = RoleType.Member, Title = "Kullanıcı Rolü"}
                };
                await context.AddRangeAsync(addingRoles);
                await context.SaveChangesAsync();
            }
        }
    }
}
