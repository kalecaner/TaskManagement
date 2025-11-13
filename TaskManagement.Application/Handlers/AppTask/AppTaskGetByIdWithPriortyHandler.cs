using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Application.Dtos;
using TaskManagement.Application.Enums;
using TaskManagement.Application.Interfaces;
using TaskManagement.Application.Requests;
using TaskManagement.Application.Results;

namespace TaskManagement.Application.Handlers.AppTask
{
    public class AppTaskGetByIdWithPriortyHandler : IRequestHandler<AppTaskGetByIdWithPriortyRequest, Result<AppTaskWithPriortyListAndUserListDto>>
    {
        private readonly IAppTaskRepository _appTaskRepository;
        private readonly IPriorityRepository _priorityRepository;
        private readonly IUserRepository _userRepository;

        public AppTaskGetByIdWithPriortyHandler(IUserRepository userRepository, IAppTaskRepository appTaskRepository, IPriorityRepository priorityRepository)
        {
            _userRepository = userRepository;
            _appTaskRepository = appTaskRepository;
            _priorityRepository = priorityRepository;
        }

        public async Task<Result<AppTaskWithPriortyListAndUserListDto>> Handle(AppTaskGetByIdWithPriortyRequest request, CancellationToken cancellationToken)
        {
            var proirities = await _priorityRepository.GetAllAsync();
            var users = await _userRepository.GetListByFilterAsync(x => x.AppRoleId != (int)RoleType.Admin);
            var appTask = await _appTaskRepository.GetByFilterAsync(x => x.Id == request.Id);
            if (appTask != null)
            {
                return new Result<AppTaskWithPriortyListAndUserListDto>(new AppTaskWithPriortyListAndUserListDto(appTask.Id, appTask.Title, appTask.Description, appTask.PriorityId.GetValueOrDefault(), appTask.AppUserId, proirities, users), true, null, null);
            }
            else
            {
                return new Result<AppTaskWithPriortyListAndUserListDto>(null, false, "No Task found", null);
            }
        }
    }
}
