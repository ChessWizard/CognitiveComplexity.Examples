using Core.Dto;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public interface IUserRoleRepository
    {
        Task AddRoleToUserAsync(List<RoleDto> roles, User user);
    }
}
