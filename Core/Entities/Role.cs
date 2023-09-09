using Core.Entities.Common;
using Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Role : BaseEntity<Guid>
    {
        public string Title { get; set; }
        public RoleType RoleType { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }
    }
}
