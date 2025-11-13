using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Application.Dtos;
using TaskManagement.Application.Interfaces;
using TaskManagement.Application.Requests;

namespace TaskManagement.Application.Handlers.AppTask
{
    public class AppTaskDeleteHandler : IRequestHandler<AppTaskDeleteRequest, Result<NoData>>
    {
        private readonly IAppTaskRepository _appTaskRepository;

        public AppTaskDeleteHandler(IAppTaskRepository appTaskRepository)
        {
            _appTaskRepository = appTaskRepository;
        }

        public async Task<Result<NoData>> Handle(AppTaskDeleteRequest request, CancellationToken cancellationToken)
        {
            var  DeletedEntity = await _appTaskRepository.GetByFilterAsync(x=>x.Id==request.Id);
            if (DeletedEntity == null)
            {
                return new Result<NoData>(new NoData(), false, "Task not found.", null);
            }
            else
            {
                try
                {
                    await _appTaskRepository.DeleteByIdAsync(DeletedEntity);
                    int result = await _appTaskRepository.SaveChangeAsync();
                    if (result > 0)
                    {

                    }
                    return new Result<NoData>(new NoData(), true, null, null);
                }
                catch (Exception ex)
                {

                    return new Result<NoData>(new NoData(), false, $"An error occurred while deleting the task: {ex.Message}", null);
                }
            }

        }
    }
}
