using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Application.Dtos;
using TaskManagement.Application.Extensions;
using TaskManagement.Application.Interfaces;
using TaskManagement.Application.Requests;
using TaskManagement.Application.Results;

namespace TaskManagement.Application.Handlers.AppTask
{
    public class AppTaskListHandler : IRequestHandler<AppTaskListRequest, PagedResult<AppTaskDto>>
    {
        private readonly IAppTaskRepository _appTaskRepository;

        public AppTaskListHandler(IAppTaskRepository appTaskRepository)
        {
            _appTaskRepository = appTaskRepository;
        }

        public async Task<PagedResult<AppTaskDto>> Handle(AppTaskListRequest request, CancellationToken cancellationToken)
        {
            var appTasks = await _appTaskRepository.GetAllAsyncByPage(request.activePage,request.SearchString,5);
            var appTaskDtos =  appTasks.Data.ToMap();

            return  new PagedResult<AppTaskDto>(appTaskDtos, request.activePage,appTasks.PageSize,appTasks.TotalPages);



        }
    }
}
