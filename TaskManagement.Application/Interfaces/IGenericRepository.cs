using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace TaskManagement.Application.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<int> CreateAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteByIdAsync(T entity);       
        Task<List<T>> GetAllAsync();
        Task<T?> GetByFilterAsync(Expression<Func<T, bool>> filter);
        Task<T?> GetByFilterAsNoTrackingAsync(Expression<Func<T, bool>> filter);
        Task<int> SaveChangeAsync();
        Task<List<T>> GetListByFilterAsync(Expression<Func<T, bool>> filter);
        Task<List<T>> GetListAsNoTrackingByFilterAsync(Expression<Func<T, bool>> filter);
        Task<int> CountByFilterAsync(Expression<Func<T, bool>> filter);
    }
}