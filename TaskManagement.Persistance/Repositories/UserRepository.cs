using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Application.Dtos;
using TaskManagement.Application.Enums;
using TaskManagement.Application.Interfaces;
using TaskManagement.Domain.Entities;
using TaskManagement.Persistance.Context;
using TaskManagement.Persistance.Extensions;

namespace TaskManagement.Persistance.Repositories
{
    public class UserRepository : GenericRepository<AppUser>, IUserRepository
    {
        

        public UserRepository(TaskManagementContext context) : base(context)
        {
            
        }

        public async Task<PagedData<AppUser>> GetAllAsyncByPage(int activePage, int roleType, string? searchString =null , int pageSize = 10)
        {
            var query = _context.Users.Where(x=>x.AppRoleId==roleType).AsQueryable();
            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(x => x.Name.ToLower().Contains(searchString.ToLower()) || x.Surname.ToLower().Contains(searchString.ToLower()));
            }
            return await query.AsNoTracking().ToPagedAsync(activePage, pageSize);
        }

        public async Task<bool> ExistsByNormalizedUserNameAsync(string normalized)
        {
           return await _context.Users.AnyAsync(x => x.NormalizedUsername == normalized);
        }

       
    }
}
