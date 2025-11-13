using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Application.Enums;
using TaskManagement.Application.Requests;
using TaskManagement.Application.Results;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Interfaces;

namespace TaskManagement.Application.Extensions
{
	public static class MappingExtension
	{
		public static AppUser ToMap(this RegisterRequest request,IUsernameNormalizer normalizer)
		{

			 return AppUser.Create(
                username: request.Username,
				password:request.Password,
				name:request.Name,
				surname:request.Surname,
				appRoleId:(int)RoleType.Member,
				email:request.Email,
				normalizer: normalizer
                );
		}
		public static Priority ToMap(this PriorityCreateRequest request)
		{
			return new Priority
			{
				Definition = request.Definition
			};
		}
		public static List<PriorityListDto> ToMap(this List<Priority> priorties)
		{
			return priorties.Select(x => new PriorityListDto(x.Id, x.Definition)).ToList();

		}

		public static AppTask ToMap(this AppTaskCreateRequest request)
		{
			return new AppTask
			{
				Title = request.Title,
				Description = request.Description,
				PriorityId = request.PriorityId,
				State = false


			};
		}
		public static List<AppTaskDto> ToMap(this List<AppTask> tasks)
		{
			return tasks.Select(x => new AppTaskDto(
				x.Id,
				x.Title,
				x.Description,
				x.Priority?.Definition,
				x.State,
				x.AppUser?.Id,
				x.AppUser != null ? $"{x.AppUser.Name}" + " " + $"{x.AppUser.Surname}" : null
				  )).ToList();
		}
		public static List<TaskReportListDto> ToMap(this List<TaskReport>  dto)
		{
			return dto.Select(x => new TaskReportListDto(
                      x.Id,
                      x.Definition,
                      x.Detail,
                      x.AppTaskId
                  )).ToList();
        }
        public static AppUser ToMap(this CreateUserRequest request, IUsernameNormalizer normalizer)
        {

            return AppUser.Create(
               username: request.Username,
               password: request.Password,
               name: request.Name,
               surname: request.Surname,
               appRoleId: (int)RoleType.Member,
               email: request.Email,
               normalizer: normalizer
               );
        }

    }
}
