using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Application.Dtos;
using TaskManagement.Application.Interfaces;
using TaskManagement.Application.Requests;
using TaskManagement.Application.Results;
using TaskManagement.Domain.Entities;
using TaskManagement.Persistance.Context;
using TaskManagement.Persistance.Extensions;

namespace TaskManagement.Persistance.Repositories
{
    public class AppTaskRepository : GenericRepository<AppTask>, IAppTaskRepository
    {
       
        public AppTaskRepository(TaskManagementContext context) : base(context)
        {
        }       

        

        public async Task<PagedData<AppTask>> GetAllAsyncByPage(int activePage,string? searchString = null, int pageSize=10)
        {
            var query = _context.Tasks.Include(x=>x.AppUser).AsQueryable();
            if(!string.IsNullOrEmpty(searchString))
            {
              query= query.Where(x => x.Title.ToLower().Contains(searchString.ToLower()));
            }
           
                return await query.Include(x => x.Priority).AsNoTracking().ToPagedAsync(activePage, pageSize);
           
              
        }
      
    }
 
}

