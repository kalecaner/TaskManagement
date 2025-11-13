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
    public class PriorityUpdateHandler : IRequestHandler<PriorityUpdateRequest, Result<NoData>>
    {
        private readonly IPriorityRepository _priorityRepository;

        public PriorityUpdateHandler(IPriorityRepository priorityRepository)
        {
            _priorityRepository = priorityRepository;
        }

        public async Task<Result<NoData>> Handle(PriorityUpdateRequest request, CancellationToken cancellationToken)
        {
            var  validator = new PriorityUpdateRequestValidator();
             var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (validationResult.IsValid)
            {
                var UpdatedEntity = await _priorityRepository.GetByFilterAsync(x => x.Id == request.Id);
                if (UpdatedEntity == null)
                {
                    return new Result<NoData>(null, false, "Priority not found", null);
                }
                else
                {
                    UpdatedEntity.Definition = request.Definition ?? "";
                    await _priorityRepository.SaveChangeAsync();

                    return new Result<NoData>(null, true, "Priority updated successfully", null);

                }
                
            }
            else
            {
                var errors = validationResult.Errors.ToMap();
                return new Result<NoData>(new NoData(), false, "Validation failed.", errors);
            }
               
               
            
        }
    }
}
