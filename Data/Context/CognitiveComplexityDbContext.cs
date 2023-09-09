using Core.Entities;
using Core.Entities.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Context
{
    public class CognitiveComplexityDbContext : DbContext
    {
        public CognitiveComplexityDbContext(DbContextOptions<CognitiveComplexityDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<UserRole> UserRoles { get; set; }

        #region SaveChanges Interceptor

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            EnsureEntityType();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void EnsureEntityType()
        {
            var trackingEntities = ChangeTracker.Entries();

            foreach (var entity in trackingEntities)
            {
                if (entity.State is EntityState.Added && entity.Entity is IAuditEntity addedEntity)
                    addedEntity.CreatedDate = DateTimeOffset.UtcNow;

                if (entity.State is EntityState.Modified && entity.Entity is IAuditEntity modifiedEntity)
                    modifiedEntity.ModifiedDate = DateTimeOffset.UtcNow;
            }
        }

        #endregion
    }
}
