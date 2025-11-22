using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TaskManagement.Application.Requests;
using TaskManagement.UI.Models;

namespace TaskManagement.UI.Areas.Member.Controllers
{
	[Area("Member")]
	[Authorize(Roles ="Member")]
	public class HomeController : Controller
	{
        private readonly IMediator _mediator;

        public HomeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Area("Member")]
		public async Task<IActionResult> Index()
		{
			var UserId = Convert.ToInt32(User.Claims.SingleOrDefault(c => c.Type == "UserId")?.Value);
			var result= await  _mediator.Send( new GetDashboardRequest(UserId));
            if (result.isSuccess)
            {

                var vm = new DashboardVm()
                {
                    NotificationCount= result.data.NotificationCount,
                    TaskCount = result.data.TaskCount,
                    UserCount= result.data.UserCount


                };
                return View(vm);
            }
            else
            {
                if (result.Errors.Count > 0)
                {
                    result.Errors.ForEach(x => ModelState.AddModelError(x.PropertyName, x.ErrorMessage));

                }
                else
                {
                    ModelState.AddModelError(string.Empty, result.ErrorMessage ?? "System error Contact Your IT department");

                }
                return View();
            }

            
		}
	}
}
