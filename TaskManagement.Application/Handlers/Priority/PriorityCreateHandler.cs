using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Application.Dtos;
using TaskManagement.Application.Extensions;
using TaskManagement.Application.Interfaces;
using TaskManagement.Application.Requests;
using TaskManagement.Application.Validators.Priority;

namespace TaskManagement.Application.Handlers.Priority
{
	public class PriorityCreateHandler : IRequestHandler<PriorityCreateRequest, Result<NoData>>
	{
		private readonly IPriorityRepository _priorityRepository;

		public PriorityCreateHandler(IPriorityRepository priorityRepository)
		{
			_priorityRepository = priorityRepository;
		}

		public async Task<Result<NoData>> Handle(PriorityCreateRequest request, CancellationToken cancellationToken)
		{
			var validator = new PriorityCreateRequestValidator();
			var validationResult = await validator.ValidateAsync(request);
			if (validationResult.IsValid)
			{

				var rowCount = await _priorityRepository.CreateAsync(request.ToMap());
				if (rowCount > 0)
				{
					return new Result<NoData>(new NoData(), true, null, null);
				}
				else
				{
					return new Result<NoData>(new NoData(), false, "An error occurred while creating the priority.", null);
				}
			}
			else
			{
				var  errors=validationResult.Errors.ToMap();
				return new Result<NoData>(new NoData(), false, "Validation failed.", errors);

			}
		}
	}
}
