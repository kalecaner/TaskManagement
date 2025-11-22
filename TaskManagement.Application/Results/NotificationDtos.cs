using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Application.Results
{
    public record NotificationDto(string Description, int? AppUserId, bool State, DateTime CreatedDate);
    public record NotificationListDto(int Id, string Description, int? AppUserId, bool State, DateTime CreatedDate);
}
