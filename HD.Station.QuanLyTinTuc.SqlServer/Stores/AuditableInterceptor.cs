using HD.Station.QuanLyTinTuc.Abstractions.Abstractions;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace HD.Station.QuanLyTinTuc.SqlServer.Stores;

public class AuditableInterceptor : SaveChangesInterceptor
{
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        var entries = eventData.Context!.ChangeTracker.Entries<IAuditableEntity>();
        foreach (var entry in entries)
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Property(a => a.CreatedAt).CurrentValue = DateTime.UtcNow;
                    entry.Property(a => a.UpdatedAt).CurrentValue = DateTime.UtcNow;
                    break;
                case EntityState.Modified:
                    entry.Property(a => a.UpdatedAt).CurrentValue = DateTime.UtcNow;
                    break;
            }
        }

        return new ValueTask<InterceptionResult<int>>(result);
    }
}
