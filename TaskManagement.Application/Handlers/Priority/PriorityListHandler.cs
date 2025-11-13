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

namespace TaskManagement.Application.Handlers.Priority
{
	public class PriorityListHandler : IRequestHandler<PriorityListRequest, Result<List<PriorityListDto>>>
	{
		private readonly IPriorityRepository _priorityRepository;

		public PriorityListHandler(IPriorityRepository priorityRepository)
		{
			_priorityRepository = priorityRepository;
		}

		public async Task<Result<List<PriorityListDto>>> Handle(PriorityListRequest request, CancellationToken cancellationToken)
		{
			var Result = await _priorityRepository.GetAllAsync();
			if (Result == null || Result.Count == 0)
			{
				return new Result<List<PriorityListDto>>(null, false, "No priorities found", null);
			}
			else
			{ 	var priorityListDtos = Result.Select(p => new PriorityListDto(p.Id, p.Definition)).ToList();
				
				return new Result<List<PriorityListDto>>(priorityListDtos, true, "Priorities retrieved successfully", null);
			}



		}
	}
}
