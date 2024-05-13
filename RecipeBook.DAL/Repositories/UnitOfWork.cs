using Microsoft.EntityFrameworkCore.Storage;
using RecipeBook.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeBook.DAL.Repositories
{
    internal class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IBaseRepository<User> Users { get; set; }
        public IBaseRepository<Role> Roles { get; set; }

        public UnitOfWork(ApplicationDbContext context, IBaseRepository<User> users, IBaseRepository<Role> roles)
        {
            _context = context;
            Users = users;
            Roles = roles;
        }


        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return await _context.Database.BeginTransactionAsync();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
