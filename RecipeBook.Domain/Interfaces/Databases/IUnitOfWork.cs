using Microsoft.EntityFrameworkCore.Storage;
using RecipeBook.Domain.Entity;

namespace RecipeBook.Domain.Interfaces.Databases
{
    public interface IUnitOfWork : IStateSaveChanges
    {
        Task<IDbContextTransaction> BeginTransactionAsync();

        IBaseRepository<User> Users { get; set; }
        IBaseRepository<Role> Roles { get; set; }
        IBaseRepository<UserRole> UserRoles { get; set; }
    }
}
