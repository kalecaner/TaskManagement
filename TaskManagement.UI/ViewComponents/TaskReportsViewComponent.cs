using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Application.Requests;
using TaskManagement.UI.Models;

namespace TaskManagement.UI.ViewComponents
{
    public class TaskReportsViewComponent: ViewComponent
    {
        private  readonly IMediator _mediator;

        public TaskReportsViewComponent(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IViewComponentResult> InvokeAsync(int appTaskId)
        {
           var result= await _mediator.Send(new TaskReportListGetByAppTaskIdRequest(appTaskId));

            var vm= result.data.Select(x=> new TaskReportListVM
            {
                AppTaskId=x.AppTaskId,
                Definition=x.Definition,
                Detail=x.Detail
            }).ToList();

            return View(vm);
        }
    }
}
