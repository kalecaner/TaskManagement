using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Application.Dtos;
using TaskManagement.Application.Interfaces;
using TaskManagement.Application.Requests;
using TaskManagement.Application.Results;

namespace TaskManagement.Application.Handlers.Notification
{
    public class NotificationListRequestHandler : IRequestHandler<NotificationListRequest, Result<List<NotificationListDto>>>
    {
        
        private readonly INotificationRepository _notificationRepository;

        public NotificationListRequestHandler(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        public async Task<Result<List<NotificationListDto>>> Handle(NotificationListRequest request, CancellationToken cancellationToken)
        {
           var result = await  _notificationRepository.GetListByFilterAsync(x=>x.AppUserId==request.UserId);

            if (result is not null)
            {
                var mappedResult = result.Select(x => new NotificationListDto(x.Id, x.Description, x.AppUserId, x.State, x.CreatedDate)).ToList();

                return new Result<List<NotificationListDto>>(mappedResult, true, null, null);

            }
            else
            {
                return new Result<List<NotificationListDto>>(null, false, "No notifications found.", null);
            }

        }
    }
}
