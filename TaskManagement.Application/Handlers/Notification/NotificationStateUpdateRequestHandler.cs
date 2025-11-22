using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Application.Dtos;
using TaskManagement.Application.Interfaces;
using TaskManagement.Application.Requests;

namespace TaskManagement.Application.Handlers.Notification
{
    public class NotificationStateUpdateRequestHandler : IRequestHandler<NotificationStateUpdateRequest, Result<NoData>>
    {
        private readonly INotificationRepository _notificationRepository;

        public NotificationStateUpdateRequestHandler(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        public async Task<Result<NoData>> Handle(NotificationStateUpdateRequest request, CancellationToken cancellationToken)
        {
             var notification =await  _notificationRepository.GetByFilterAsync(x=>x.Id==request.Id);
            if (notification == null)
            {
                return new Result<NoData>(new NoData(), true, "notification not found", null);
            }
            else
            {
                notification.State = request.State;
                await _notificationRepository.UpdateAsync(notification);
                return new Result<NoData>(new NoData(), true, null, null);
            }
        }
    }
}
