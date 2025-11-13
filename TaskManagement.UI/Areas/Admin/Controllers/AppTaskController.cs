using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;
using TaskManagement.Application.Dtos;
using TaskManagement.Application.Requests;
using TaskManagement.Application.Results;
using TaskManagement.Domain.Entities;
using TaskManagement.UI.Models;

namespace TaskManagement.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]

    public class AppTaskController : Controller
    {
        private readonly IMediator _mediator;

        public AppTaskController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IActionResult> Index(string? searchString,int activePage=1)
        {
            ViewBag.searchString = searchString;
            var result = await _mediator.Send(new AppTaskListRequest(activePage,searchString));
            return View(result);
        }
        public  async Task<IActionResult> Create()
        {
             var result = await _mediator.Send(new PriorityListRequest());
            ViewBag.Priorities =  new List<SelectListItem>(
                result.data.Select(x => new SelectListItem
                {
                    Text = x.Definition,
                    Value = x.Id.ToString()
                })
            );

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AppTaskCreateRequest request)
        {
            
         

            var result = await _mediator.Send(request);
            ViewBag.Priorities = new List<SelectListItem>(result.data.Priorities.Select(x => new SelectListItem
            {    Text = x.Definition,
                 Value = x.Id.ToString()
             })
         );
            if (result.isSuccess)
            {
                return RedirectToAction("Index");
            }
            else
            {
                if (result.Errors.Count>0)
                {
                    result.Errors.ForEach(x => ModelState.AddModelError(x.PropertyName, x.ErrorMessage));
                    
                }
                else
                {
                    ModelState.AddModelError(string.Empty, result.ErrorMessage ?? "System error Contact Your IT department");
                  
                }
                return View(request);
            }

            
        }

        public  async Task<IActionResult> Delete(int id)
        {
           await _mediator.Send(new AppTaskDeleteRequest(id));
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Update(int id)
        {
            var result = await _mediator.Send( new AppTaskGetByIdWithPriortyRequest(id));

            var vm = new AppTaskUpdateVM()
            {
                Id = id,
                Title = result.data.Title,
                Description = result.data.Description,
                PriorityId = result.data.PriorityId,
                AppUserId = result.data.AppUserId!=null ? result.data.AppUserId:null,
                Priorities = new List<SelectListItem>(result.data.Priorities.Select(x => new SelectListItem
                {
                    Text = x.Definition,
                    Value =  x.Id.ToString() ,
                    Selected = result.data.PriorityId == x.Id
                })),
                Users = new List<SelectListItem>(result.data.Users.Select(x => new SelectListItem
                {
                    Text =  $"{x.Name} {x.Surname}",
                    Value = x.Id.ToString() ,                   
                    Selected = result.data.AppUserId == x.Id
                }))
            };
            return View(vm);
        }

        public async Task<IActionResult> Detail(int id)
        {
            var result = await _mediator.Send(new AppTaskGetByIdWithPriortyRequest(id));

            var vm = new AppTaskUpdateVM()
            {
                Id = id,
                Title = result.data.Title,
                Description = result.data.Description,
                PriorityId = result.data.PriorityId,
                AppUserId = result.data.AppUserId != null ? result.data.AppUserId : null,
                Priorities = new List<SelectListItem>(result.data.Priorities.Select(x => new SelectListItem
                {
                    Text = x.Definition,
                    Value = x.Id.ToString(),
                    Selected = result.data.PriorityId == x.Id
                })),
                Users = new List<SelectListItem>(result.data.Users.Select(x => new SelectListItem
                {
                    Text = $"{x.Name} {x.Surname}",
                    Value = x.Id.ToString(),
                    Selected = result.data.AppUserId == x.Id
                }))
            };
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Update(AppTaskUpdateVM vm)
        {

            var request= new  AppTaskUpdateRequest(vm.Id,vm.Title,vm.Description,vm.PriorityId,vm.AppUserId);
            var result = await _mediator.Send(request);
         
            if (result.isSuccess)
            {
                return RedirectToAction("Index");
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
                return View(vm);
            }
           
        }
    }
}
