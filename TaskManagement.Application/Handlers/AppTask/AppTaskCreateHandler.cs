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
using TaskManagement.Application.Results;
using TaskManagement.Application.Validators.AppTask;

namespace TaskManagement.Application.Handlers.AppTask
{
    public class AppTaskCreateHandler : IRequestHandler<AppTaskCreateRequest, Result<AppTaskWithPriortyListDto>>
    {
        private readonly IAppTaskRepository _appTaskRepository;
        private readonly IPriorityRepository _priorityRepository;
        public AppTaskCreateHandler(IAppTaskRepository appTaskRepository, IPriorityRepository priorityRepository)
        {
            _appTaskRepository = appTaskRepository;
            _priorityRepository = priorityRepository;
        }

        public  async Task<Result<AppTaskWithPriortyListDto>> Handle(AppTaskCreateRequest request, CancellationToken cancellationToken)
        {
            var priority = await _priorityRepository.GetAllAsync();
            var priortyDtoList = priority.ToMap();

            var  validator= new AppTaskCreateRequestValidator();
            var validationResult=  validator.Validate(request);
            if(!validationResult.IsValid)
            {
                 return new Result<AppTaskWithPriortyListDto>(new AppTaskWithPriortyListDto(priortyDtoList),false,null,validationResult.Errors.ToMap());
                
            }
       
            else
            {

                var result = await _appTaskRepository.CreateAsync(request.ToMap());
                if(result>0)
                {
                    return new Result<AppTaskWithPriortyListDto>(new AppTaskWithPriortyListDto(priortyDtoList), true, null, null);
                }
                else
                {
                    return new Result<AppTaskWithPriortyListDto>(new AppTaskWithPriortyListDto(priortyDtoList), false, "Kayıt Eklenemedi", null);
                }
                
            }
            
        }
    }
}
