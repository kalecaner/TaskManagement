using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Application.Dtos;
using TaskManagement.Application.Results;
using TaskManagement.Domain.Entities;
using static TaskManagement.Application.Requests.ApplicationRequests;

namespace TaskManagement.Application.Requests
{
    public record AppTaskListRequest : PagedRequest, IRequest<PagedResult<AppTaskDto>>
    {
        public AppTaskListRequest(int activePage ,string? searchString) : base(activePage)
        {
            SearchString = searchString;
        }
        public string? SearchString { get; set; }
    }
   
    public record AppTaskGetByIdWithPriortyRequest(int Id) : IRequest<Result<AppTaskWithPriortyListAndUserListDto>>;
    public record AppTaskUpdateRequest(int Id,string? Title, string? Description,int? PriorityId, int? AppUserId) : IRequest<Result<NoData>>;

    public record AppTaskGetByIdRequest(int Id) : IRequest<Result<AppTaskDto>>;
                  
    public record AppTaskCreateRequest(string? Title, string? Description, int PriorityId) : IRequest<Result<AppTaskWithPriortyListDto>>;
    public record AppTaskDeleteRequest(int Id) : IRequest<Result<NoData>>;
}                 
