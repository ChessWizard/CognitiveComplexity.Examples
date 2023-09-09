using Core.Entities.Common;
using Core.Entities.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class UserRole : AuditEntity<Guid>, ISoftDeleteEntity
    {
        public bool IsDeleted { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }

        public Guid RoleId { get; set; }
        public Role Role { get; set; }
    }
}
