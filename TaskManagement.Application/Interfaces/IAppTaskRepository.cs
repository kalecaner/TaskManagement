using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Application.Dtos;
using TaskManagement.Application.Requests;
using TaskManagement.Application.Results;
using TaskManagement.Domain.Entities;

namespace TaskManagement.Application.Interfaces
{
    public interface IAppTaskRepository : IGenericRepository<AppTask>
    {
        // Sayfalama gibi AppTask'e özel metotlar
        Task<PagedData<AppTask>> GetAllAsyncByPage(int activePage, string? searchString = null, int pageSize = 10);
    }
}
