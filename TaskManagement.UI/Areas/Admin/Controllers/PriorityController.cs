using Azure.Core;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TaskManagement.Application.Dtos;
using TaskManagement.Application.Requests;

namespace TaskManagement.UI.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles = "Admin")]
	public class PriorityController : Controller
	{
		private readonly IMediator _mediator;

		public PriorityController(IMediator mediator)
		{
			_mediator = mediator;
		}

		public async Task<IActionResult> Index()
		{

			var result = await _mediator.Send(new PriorityListRequest());
			if (!result.isSuccess)
			{
				ModelState.AddModelError(string.Empty, result.ErrorMessage ?? "An error occurred while retrieving priorities.");
				return View();
			}
			else
			{
				return View(result.data);
			}

		}

		public async Task<IActionResult> Create()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Create(PriorityCreateRequest request)
		{
			var Result = await _mediator.Send(request);
			if (Result.isSuccess)
			{
				return RedirectToAction("Index");
			}
			else
			{
				if (Result.Errors != null && Result.Errors.Count > 0)
				{
					foreach (var error in Result.Errors)
					{
						ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
					}
					
				}
				else
				{
					ModelState.AddModelError(string.Empty, Result.ErrorMessage ?? "An error occurred while creating the priority.");
					
				}

				return View(request);
			}
		}
		
		public async Task<IActionResult> Delete(int Id)
		{
			await _mediator.Send(new PriorityDeleteRequest(Id));			

				return RedirectToAction("Index");
			

		}
		public async Task<IActionResult> Update(int id)
		{
			 var result= await _mediator.Send(new PriorityGetByIdRequest(id));
			if (result.isSuccess)
			{
				var  requestModel = new PriorityUpdateRequest(result.data.Id, result.data.Definition);
                return View(requestModel);
            }
			else
			{
                
                
                    ModelState.AddModelError(string.Empty, result.ErrorMessage ?? "An error occurred while updating the priority.");

               

                return View();

            }
		}

		[HttpPost]
		public async Task<IActionResult> Update(PriorityUpdateRequest request)
		{
            var Result = await _mediator.Send(request);
            if (Result.isSuccess)
            {
                return RedirectToAction("Index");
            }
            else
            {
                if (Result.Errors != null && Result.Errors.Count > 0)
                {
                    foreach (var error in Result.Errors)
                    {
                        ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                    }

                }
                else
                {
                    ModelState.AddModelError(string.Empty, Result.ErrorMessage ?? "An error occurred while updating the priority.");

                }

                return View(request);
            }
        }

	}
}
