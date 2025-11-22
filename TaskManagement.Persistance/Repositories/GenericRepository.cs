using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TaskManagement.Application.Interfaces;
using TaskManagement.Persistance.Context;

namespace TaskManagement.Persistance.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        public readonly TaskManagementContext _context;
        public readonly DbSet<T> _dbSet;

        public GenericRepository(TaskManagementContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public virtual async Task<int> CreateAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            return await _context.SaveChangesAsync();
        }

        public virtual async Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }

        public virtual async Task DeleteByIdAsync(T entity)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }
        

        public virtual async Task<List<T>> GetAllAsync()
        {
            return await _dbSet.AsNoTracking().ToListAsync();
        }

        public virtual async Task<T?> GetByFilterAsync(Expression<Func<T, bool>> filter)
        {
            return await _dbSet.SingleOrDefaultAsync(filter);
        }
        public virtual async Task<List<T>> GetListByFilterAsync(Expression<Func<T, bool>> filter)
        {
            return await _dbSet.Where(filter).ToListAsync();
        }
        public virtual async Task<List<T>> GetListAsNoTrackingByFilterAsync(Expression<Func<T, bool>> filter)
        {
            return await _dbSet.AsNoTracking().Where(filter).ToListAsync();
        }

        public virtual async Task<T?> GetByFilterAsNoTrackingAsync(Expression<Func<T, bool>> filter)
        {
            return await _dbSet.AsNoTracking().SingleOrDefaultAsync(filter);
        }

        public virtual Task<int> SaveChangeAsync()
        {
            return _context.SaveChangesAsync();
        }

        public Task<int> CountByFilterAsync(Expression<Func<T, bool>> filter)
        {
            return _dbSet.CountAsync(filter);
        }
    }
}