using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Application.Dtos;
using TaskManagement.Application.Results;

namespace TaskManagement.Application.Requests
{
    public record NotificationUpdateAppTaskRequest(int AppTaskId, string Title, string Description, int PriorityId, int? AppUserId, bool IsRead = false): IRequest<Result<NoData>>;
    public record NotificationListRequest(int UserId) : IRequest<Result<List<NotificationListDto>>>;
    public record NotificationStateUpdateRequest(int Id, bool State) : IRequest<Result<NoData>>;


}