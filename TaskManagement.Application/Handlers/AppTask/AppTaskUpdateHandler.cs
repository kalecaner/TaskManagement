using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Application.Dtos;
using TaskManagement.Application.Enums;
using TaskManagement.Application.Extensions;
using TaskManagement.Application.Interfaces;
using TaskManagement.Application.Requests;
using TaskManagement.Application.Results;
using TaskManagement.Application.Validators.AppTask;
using TaskManagement.Domain.Entities;

namespace TaskManagement.Application.Handlers.AppTask
{
    public class AppTaskUpdateHandler : IRequestHandler<AppTaskUpdateRequest, Result<NoData>>
    {
        private readonly IAppTaskRepository _appTaskRepository;
        private readonly INotificationRepository _notificationRepository;
        private readonly IUserRepository _userRepository;

        public AppTaskUpdateHandler(IAppTaskRepository appTaskRepository, INotificationRepository notificationRepository, IUserRepository userRepository)
        {
            _appTaskRepository = appTaskRepository;
            _notificationRepository = notificationRepository;
            _userRepository = userRepository;
        }

        public async Task<Result<NoData>> Handle(AppTaskUpdateRequest request, CancellationToken cancellationToken)
        {
           var validator= new AppTaskUpdateRequestValidator();
              var validationResult= validator.Validate(request);
            if(!validationResult.IsValid)
                {
                return await  Task.FromResult(new Result<NoData>(new NoData(),false,null,validationResult.Errors.ToList().Select(x=> new ValidationError(x.PropertyName,x.ErrorMessage)).ToList()));
            }
            else
            {
                var entity = await _appTaskRepository.GetByFilterAsync(x=>x.Id==request.Id);

                string  fullName = entity.AppUser!= null ? (entity.AppUser.Name  + " " + entity.AppUser.Surname) :"Atanmamış";
                string NewFullName= "Atanmamış";

                var oldEntity =  new AppTaskItemDto(entity.Id, entity.Title, entity.Description, entity.PriorityId, entity.AppUserId, fullName);
               

                entity.Title = request.Title;
                entity.Description = request.Description;
                entity.PriorityId = request.PriorityId;
                entity.AppUserId = request.AppUserId;

                if (fullName != null)
                {
                    var user = await _userRepository.GetByFilterAsNoTrackingAsync(x => x.Id == request.AppUserId);
                    NewFullName=user.Name + " " + user.Surname;

                }
                  


                #region NotificationArea
                var changes = new List<string>();
                var comparisons = new List<(object OldValue, object NewValue, string fieldName)>
             {

               (oldEntity.Description, entity.Description, "Description"),
               (oldEntity.Title, entity.Title, "Title"),
               (oldEntity.PriorityId, entity.PriorityId, "PriorityId"),
               (oldEntity.Fullname, NewFullName, "User Name ")


             };
                foreach (var (OldValue, NewValue, fieldName) in comparisons)
                {
                    if (!Equals(OldValue, NewValue))
                    {
                        changes.Add($"  {oldEntity.Title}  isimli Task  içerisinde  '{fieldName}' alanı '{OldValue}' dan '{NewValue}' olacak şekilde değiştirildi");
                    }
                }
                var dto = new NotificationDto("", entity.AppUserId, false, DateTime.UtcNow);
                if (changes.Count == 0)
                {
                    return new Result<NoData>(null, true, "No changes detected", null);
                }
                else if (changes.Count == 1)
                {
                    dto = new NotificationDto(changes[0].ToString(), entity.AppUserId, false, DateTime.UtcNow);

                }
                else
                {
                    dto = new NotificationDto(entity.Title + " taskı ile alakalı yeni güncellemeler var ", entity.AppUserId, false, DateTime.UtcNow);

                }
                var resultNotification = await _notificationRepository.CreateAsync(dto.ToMap());
                if (resultNotification > 0)
                {
                    return new Result<NoData>(new NoData(), true, null, null);
                }
                else
                {
                    return new Result<NoData>(null, false, "Failed to create notification", null);
                }

                #endregion

                int result = await _appTaskRepository.SaveChangeAsync();
                if (result>0)
                {
                    return await Task.FromResult(new Result<NoData>(new NoData(), true, null, null));
                }
                else
                {
                    return await Task.FromResult(new Result<NoData>(new NoData(), false, "Kayıt Güncellenemedi", null));
                }
            }

        }
    }
}
