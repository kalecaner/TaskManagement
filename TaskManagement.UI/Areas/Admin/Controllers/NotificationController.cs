using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TaskManagement.Application.Requests;
using TaskManagement.Application.Results;
using TaskManagement.UI.Models;

namespace TaskManagement.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class NotificationController : Controller
    {
        private readonly IMediator _mediator;

        public NotificationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IActionResult> Index()
        {          
            var userId =Convert.ToInt32(User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value);
            var  result= await  _mediator.Send(new NotificationListRequest(userId));
           var notifications = result.data.Select(x=> new NotificationVM
           {
               CreatedDate=x.CreatedDate,
                Description=x.Description,
                Id=x.Id,
                State=x.State

           }).ToList();
             return View(notifications);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateIsRead(int Id,bool isRead)
        {
            var result = await _mediator.Send(new NotificationStateUpdateRequest(Id,isRead));
            if (!result.isSuccess)
            {
                return Ok(new { success = false, newStatus = isRead });
            }

            return Ok(new { success = true, newStatus = isRead });
        }
    }
}