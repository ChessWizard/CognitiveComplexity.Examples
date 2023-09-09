using Core.Entities.Common;
using Core.Entities.Common.Interfaces;
using Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class User : AuditEntity<Guid>, ISoftDeleteEntity
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public AccountType AccountType { get; set; }

        public bool IsDeleted { get; set; }

        public ICollection<UserRole> UserRoles { get; set; }
    }
}
