using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Domain.Entities;

namespace TaskManagement.Application.Results
{
    public record AppTaskDto(int Id, string Title,string Description,string PriorityDefinition,bool State,int? AppUserId, string? AppFullname);
    public record AppTaskWithPriortyListDto (List<PriorityListDto> Priorities);
    public record AppTaskWithPriortyListAndUserListDto(int Id, string? Title, string? Description, int PriorityId, int? AppUserId,List<Priority> Priorities, List<AppUser> Users);
    public record AppTaskItemDto(int Id, string? Title, string? Description, int? PriorityId, int? AppUserId,string Fullname);

}


