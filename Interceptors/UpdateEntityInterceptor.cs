using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using WebAPI.Models.Common;

namespace WebAPI.Interceptors
{
    public class UpdateEntityInterceptor : SaveChangesInterceptor
    {
        //public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        //{
        //    DbContext? dbContext = eventData.Context;

        //    if (dbContext is null)
        //    {
        //        return base.SavingChangesAsync(eventData, result, cancellationToken);
        //    }

        //    IEnumerable<EntityEntry<AuditableEntity>> auditableEntities = dbContext
        //                                                                    .ChangeTracker
        //                                                                    .Entries<AuditableEntity>();

        //    foreach (var entity in auditableEntities)
        //    {
        //        if (entity.State == EntityState.Added)
        //        {
        //            entity.Property(a => a.Created).CurrentValue = DateTime.UtcNow;
        //        }
        //        if (entity.State == EntityState.Modified)
        //        {
        //            entity.Property(a => a.LastModified).CurrentValue = DateTime.UtcNow;
        //        }
        //    }

        //    return base.SavingChangesAsync(eventData, result, cancellationToken);
        //}
    }
}
