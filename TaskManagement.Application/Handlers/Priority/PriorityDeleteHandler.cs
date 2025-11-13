using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Application.Dtos;
using TaskManagement.Application.Interfaces;
using TaskManagement.Application.Requests;

namespace TaskManagement.Application.Handlers.Priority
{
    public class PriorityDeleteHandler : IRequestHandler<PriorityDeleteRequest, Result<NoData>>
    {
        private readonly IPriorityRepository _priorityRepository;

        public PriorityDeleteHandler(IPriorityRepository priorityRepository)
        {
            _priorityRepository = priorityRepository;
        }

        public async Task<Result<NoData>> Handle(PriorityDeleteRequest request, CancellationToken cancellationToken)
        {
            var DeletedEntity = await _priorityRepository.GetByFilterAsync(x => x.Id == request.Id);
            if (DeletedEntity == null)
            {
                return  new Result<NoData>(new NoData(), false, "Priority not found.",null);
            }
            else
            {
                try
                {
                  await _priorityRepository.DeleteByIdAsync(DeletedEntity);
                    return new Result<NoData>(new NoData(), true, null, null);
                }
                catch (Exception ex)
                {
                    return new Result<NoData>( new NoData(),false,$"An error occurred while deleting the priority: {ex.Message}",null);
                }


            }
               

        }
    }
}
