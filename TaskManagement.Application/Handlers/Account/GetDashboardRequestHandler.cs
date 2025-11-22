using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Application.Dtos;
using TaskManagement.Application.Interfaces;
using TaskManagement.Application.Requests;
using TaskManagement.Application.Results;

namespace TaskManagement.Application.Handlers.Account
{
    public class GetDashboardRequestHandler : IRequestHandler<GetDashboardRequest, Result<DashboardDto>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IAppTaskRepository _taskRepository;   
        private readonly INotificationRepository _notificationRepository;

        public GetDashboardRequestHandler(IUserRepository userRepository, IAppTaskRepository taskRepository, INotificationRepository notificationRepository)
        {
            _userRepository = userRepository;
            _taskRepository = taskRepository;
            _notificationRepository = notificationRepository;
        }

        public async Task<Result<DashboardDto>> Handle(GetDashboardRequest request, CancellationToken cancellationToken)
        {
           var taskCount =await _taskRepository.CountByFilterAsync(x=>x.AppUserId==request.UserId);
           var userCount = await _userRepository.CountByFilterAsync(x=>true);
           var notificationCount = await _notificationRepository.CountByFilterAsync(x => x.AppUserId == request.UserId && x.State==false);

            var dashboardDto = new DashboardDto(
                TaskCount: taskCount,
                UserCount: userCount,
                NotificationCount: notificationCount);


            return new Result<DashboardDto>(dashboardDto, isSuccess: true, ErrorMessage: null, Errors: null);



        }
    }
}
