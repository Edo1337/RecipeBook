﻿using Microsoft.EntityFrameworkCore;

namespace RecipeBook.DAL
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        private readonly ApplicationDbContext _dbContext;

        public BaseRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<TEntity> GetAll()
        {
            return _dbContext.Set<TEntity>();
        }

        public async Task<TEntity> CreateAsync(TEntity entity)
        {
            if(entity == null)
                throw new ArgumentNullException("Ошибка добавления сущности: Сущности не существует");

            await _dbContext.AddAsync(entity);
            await _dbContext.SaveChangesAsync();

            return entity;
        }

        public void Remove(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException("Ошибка удаления сущности: Сущности не существует");

            _dbContext.Remove(entity);
        }

        public TEntity Update(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException("Ошибка обновления сущности: Сущности не существует");

            _dbContext.Update(entity);

            return entity;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}
