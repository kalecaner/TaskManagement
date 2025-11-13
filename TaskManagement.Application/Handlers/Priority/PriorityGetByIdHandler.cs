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
    public class PriorityGetByIdHandler : IRequestHandler<PriorityGetByIdRequest, Result<PriorityListDto>>
    {
        private readonly IPriorityRepository _priorityRepository;
        public PriorityGetByIdHandler(IPriorityRepository priorityRepository)
        {
            _priorityRepository = priorityRepository;
        }

        public async Task<Result<PriorityListDto>> Handle(PriorityGetByIdRequest request, CancellationToken cancellationToken)
        {
            var priority = await  _priorityRepository.GetByFilterAsync(x => x.Id == request.Id);
               
            if (priority == null)
            {
                return new Result<PriorityListDto>(null, false, "Priority not found.", null);
            }
            else
            {
                var priorityDto = new PriorityListDto(priority.Id, priority.Definition);                
                return new Result<PriorityListDto>(priorityDto, true, null, null);
            }
        }
    }
}
