﻿using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace SmartERP.CommonTools.Repositories
{
    public abstract class BaseRepository : IBaseRepository
    {
        private readonly DbContext _context;

        public BaseRepository(DbContext context)
        {
            _context = context;    
        }

        public TEntity Add<TEntity>(TEntity entity) where TEntity : class
        {
            _context.Set<TEntity>().Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public IEnumerable<TEntity> AddRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
        {
            _context.Set<TEntity>().AddRange(entities);
            _context.SaveChanges();
            return entities;
        }

        public void Update<TEntity>(TEntity entity) where TEntity : class
        {
            var keyName = _context.Model.FindEntityType(typeof(TEntity))?
                             .FindPrimaryKey()?
                             .Properties
                             .Select(x => x.Name)
                             .Single();

            if (keyName == null)
            {
                throw new InvalidOperationException($"Entity {typeof(TEntity).Name} does not have a single primary key property.");
            }
            var keyProperty = typeof(TEntity).GetProperty(keyName);
            if (keyProperty == null)
            {
                throw new InvalidOperationException($"Entity {typeof(TEntity).Name} does not have a key property.");
            }

            var keyValue = keyProperty.GetValue(entity);

            var existingEntity = _context.Set<TEntity>().Local
                .FirstOrDefault(e => keyProperty.GetValue(e).Equals(keyValue));

            if (existingEntity != null)
            {
                _context.Entry(existingEntity).State = EntityState.Detached;
            }

            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Remove<TEntity>(TEntity entity) where TEntity : class
        {
            _context.Set<TEntity>().Remove(entity);
            _context.SaveChanges();
        }

        public void RemoveRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
        {
            _context.Set<TEntity>().RemoveRange(entities);
            _context.SaveChanges();
        }

        public TEntity? GetById<TEntity>(long id) where TEntity : class
        {
            return _context.Set<TEntity>().Find(id);            
        }

        public async Task<TEntity?> GetByIdAsync<TEntity>(long id) where TEntity : class
        {
            return await _context.Set<TEntity>().FindAsync(id);            
        }

        public TEntity? Get<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class
        {
            return _context.Set<TEntity>().FirstOrDefault(predicate);
        }

        public async Task<TEntity?> GetAsync<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class
        {
            return await _context.Set<TEntity>().FirstOrDefaultAsync(predicate);
        }

        public IEnumerable<TEntity> GetList<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class
        {
            return _context.Set<TEntity>().Where(predicate);
        }

        public async Task<IEnumerable<TEntity>> GetListAsync<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class
        {
            return await Task.Run(() => _context.Set<TEntity>().Where(predicate));
        }

        public IEnumerable<TEntity> GetAll<TEntity>() where TEntity : class
        {
            return _context.Set<TEntity>().ToList();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync<TEntity>() where TEntity : class
        {
            return await Task.Run(() => _context.Set<TEntity>().ToList());
        }

        public int Count<TEntity>() where TEntity : class
        {
            return _context.Set<TEntity>().Count();
        }

        public Task<int> CountAsync<TEntity>() where TEntity : class
        {
            return _context.Set<TEntity>().CountAsync();
        }


        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
