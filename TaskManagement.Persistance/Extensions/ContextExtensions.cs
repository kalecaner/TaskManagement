using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Application.Dtos;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace TaskManagement.Persistance.Extensions
{
    public static class ContextExtensions
    {
        public static async Task<PagedData<T>> ToPagedAsync<T>(this IQueryable<T> query, int activePage, int pageSize) where T : class, new()
        {
            var list=await query.AsNoTracking().Skip((activePage-1) * pageSize).Take(pageSize).ToListAsync();

             int count= await query.AsNoTracking().CountAsync();
            return new PagedData<T>(list, activePage, pageSize,  count);
        }
    }
}
