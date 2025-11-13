using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Application.Dtos;
using TaskManagement.Domain.Entities;

namespace TaskManagement.Application.Interfaces
{
	public interface IUserRepository:IGenericRepository<AppUser>
    {
        Task<PagedData<AppUser>> GetAllAsyncByPage(int activePage, int roleType, string? searchString = null, int pageSize = 10);
        Task<bool> ExistsByNormalizedUserNameAsync(string normalized);
       


    }


}

