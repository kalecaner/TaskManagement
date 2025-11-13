using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Application.Dtos;
using TaskManagement.Application.Enums;
using TaskManagement.Application.Interfaces;
using TaskManagement.Application.Requests;
using TaskManagement.Application.Results;
using TaskManagement.Application.Validators.AppTask;

namespace TaskManagement.Application.Handlers.AppTask
{
    public class AppTaskUpdateHandler : IRequestHandler<AppTaskUpdateRequest, Result<NoData>>
    {
        private readonly IAppTaskRepository _appTaskRepository;

        public AppTaskUpdateHandler(IAppTaskRepository appTaskRepository)
        {
            _appTaskRepository = appTaskRepository;
        }

        public async Task<Result<NoData>> Handle(AppTaskUpdateRequest request, CancellationToken cancellationToken)
        {
           var validator= new AppTaskUpdateRequestValidator();
              var validationResult= validator.Validate(request);
            if(!validationResult.IsValid)
                {
                return await  Task.FromResult(new Result<NoData>(new NoData(),false,null,validationResult.Errors.ToList().Select(x=> new ValidationError(x.PropertyName,x.ErrorMessage)).ToList()));
            }
            else
            {
                var entity = await _appTaskRepository.GetByFilterAsync(x=>x.Id==request.Id);
                if(entity==null)
                {
                    return await Task.FromResult(new Result<NoData>(new NoData(), false, "Kayıt Bulunamadı", null));
                }
                entity.Title = request.Title;
                entity.Description = request.Description;
                entity.PriorityId = request.PriorityId;
                entity.AppUserId = request.AppUserId;
               
               
               int result= await _appTaskRepository.SaveChangeAsync();
                if (result>0)
                {
                    return await Task.FromResult(new Result<NoData>(new NoData(), true, null, null));
                }
                else
                {
                    return await Task.FromResult(new Result<NoData>(new NoData(), false, "Kayıt Güncellenemedi", null));
                }
            }

        }
    }
}
