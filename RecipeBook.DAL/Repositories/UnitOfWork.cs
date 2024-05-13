using Microsoft.EntityFrameworkCore.Storage;
using RecipeBook.Domain.Entity;
using RecipeBook.Domain.Interfaces.Databases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeBook.DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IBaseRepository<User> Users { get; set; }
        public IBaseRepository<Role> Roles { get; set; }
        public IBaseRepository<UserRole> UserRoles { get; set; }

        public UnitOfWork(ApplicationDbContext context, IBaseRepository<User> users, IBaseRepository<Role> roles, IBaseRepository<UserRole> userRoles)
        {
            _context = context;
            Users = users;
            Roles = roles;
            UserRoles = userRoles;
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
