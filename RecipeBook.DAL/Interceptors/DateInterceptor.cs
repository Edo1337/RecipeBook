using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace RecipeBook.DAL.Interceptors
{
    internal class DateInterceptor : SaveChangesInterceptor
    {
        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            var dbContext = eventData.Context;
            if (dbContext == null)
            {
                return base.SavingChanges(eventData, result);
            }

            //Хранит свойства IAuditable
            var entries = dbContext.ChangeTracker.Entries<IAuditable>();
            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property(x => x.CreatedAt).CurrentValue = DateTime.UtcNow;
                }
                if (entry.State == EntityState.Modified)
                {
                    entry.Property(x => x.UpdatedAt).CurrentValue = DateTime.UtcNow;
                }
            }


            return base.SavingChanges(eventData, result);
        }
    }
}
