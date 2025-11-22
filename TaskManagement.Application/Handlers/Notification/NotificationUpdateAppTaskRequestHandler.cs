using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Application.Dtos;
using TaskManagement.Application.Extensions;
using TaskManagement.Application.Interfaces;
using TaskManagement.Application.Requests;
using TaskManagement.Application.Results;

namespace TaskManagement.Application.Handlers.Notification
{
    public class NotificationUpdateAppTaskRequestHandler : IRequestHandler<NotificationUpdateAppTaskRequest, Result<NoData>>
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly IAppTaskRepository _appTaskRepository;

        public NotificationUpdateAppTaskRequestHandler(INotificationRepository notificationRepository, IAppTaskRepository appTaskRepository)
        {
            _notificationRepository = notificationRepository;
            _appTaskRepository = appTaskRepository;
        }

        public async Task<Result<NoData>> Handle(NotificationUpdateAppTaskRequest request, CancellationToken cancellationToken)
        {
            var appTask= await _appTaskRepository.GetByFilterAsNoTrackingAsync(x=>x.Id==request.AppTaskId);
            if (appTask == null)
            {
                return new Result<NoData>(null,false, "Task not found", null);  
                
            }
             var changes = new List<string>();
            var comparisons = new List<(object OldValue, object NewValue, string fieldName)>
             {

               (appTask.Description, request.Description, "Description"),
               (appTask.Title, request.Title, "Title"),
               (appTask.PriorityId, request.PriorityId, "PriorityId"),
               (appTask.AppUserId, request.AppUserId, "AppUserId")


             };
            foreach (var (OldValue, NewValue, fieldName) in comparisons)
            {
                if (!Equals(OldValue, NewValue))
                {
                    changes.Add($" At {appTask.Title} Task , Field '{fieldName}' changed from '{OldValue}' to '{NewValue}'");
                }
            }
            var dto = new NotificationDto("", request.AppTaskId, false, DateTime.UtcNow);
            if (changes.Count == 0)
            {
                return new Result<NoData>(null, true, "No changes detected", null);
            }            
            else if (changes.Count == 1)
            {
                 dto= new NotificationDto(changes[0].ToString(),request.AppTaskId,false,DateTime.UtcNow);
                
            }
            else
            {
                 dto = new NotificationDto(appTask.Title+" taskı ile alakalı yeni güncellemeler var ", request.AppTaskId, false, DateTime.UtcNow);
               
            }
            var result = await _notificationRepository.CreateAsync(dto.ToMap());
            if (result >0)
            {
                return new Result<NoData>(new NoData(), true, null, null);
            }
            else
            {
                return new Result<NoData>(null, false, "Failed to create notification", null);
            }
        }
    }
}
