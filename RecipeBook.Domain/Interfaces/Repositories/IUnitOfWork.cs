using Microsoft.EntityFrameworkCore.Storage;
using RecipeBook.Domain.Entity;

namespace RecipeBook
{
    public interface IUnitOfWork: IDisposable
    {
        Task<IDbContextTransaction> BeginTransactionAsync();

        Task<int> SaveChangesAsync();

        IBaseRepository<User> Users { get; set; }
        IBaseRepository<Role> Roles { get; set; }

    }
}
