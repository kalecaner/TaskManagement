using Azure.Core;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TaskManagement.Application.Requests;
using TaskManagement.UI.Models;

namespace TaskManagement.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IActionResult> IndexAsync(string? searchString, int activePage = 1)
        {
            ViewBag.searchString = searchString;
            var result = await _mediator.Send(new MemberPageListRequest(activePage, searchString));
            if (result?.Data == null || !result.Data.Any())
            {

                return View();
            }
            else
            {
                UserListPagedVM user = new UserListPagedVM()
                {
                    ActivePage = result.ActivePage,
                    PageSize = result.PageSize,
                    TotalPages = result.TotalPages,
                    Users = result.Data.Select(x => new UsersVM(x.Id, x.Name, x.Surname, x.Username, (int)x.Role)).ToList()
                };


                return View(user);

            }

        }
        public IActionResult Create()
        {
            CreateUserVM vm = new CreateUserVM();

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserVM userVM)
        {
            var result = await _mediator.Send(new CreateUserRequest(userVM.Username, userVM.Password, userVM.ConfirmPassword, userVM.Email, userVM.Name, userVM.Surname, userVM.RoleId));
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
                return View(userVM);
            }
        }

        public async Task<IActionResult> SendPasswordInfo(string id)
        {

            return View(model: id);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SendPasswordReset(string id)
        {

            var result = await _mediator.Send(new SendPasswordResetMailRequest(id));
            if (result.isSuccess)
            {

                return RedirectToAction("SendPasswordInfo", "User", new { area = "Admin", id });
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
                return RedirectToAction("Index");
            }
        }

        public async Task<IActionResult> Update(int id)
        {
            var UpdateUserData = await _mediator.Send(new GetUserByIdRequest(id));
            var vm = new UpdateUserVM()
            {
                Id = UpdateUserData.data!.Id,
                Name = UpdateUserData.data!.Name,
                Surname = UpdateUserData.data!.Surname,
                Username = UpdateUserData.data!.Username,
                Email = UpdateUserData.data!.Email,
                RoleId = (int)UpdateUserData.data!.Role
            };
            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> Update(UpdateUserVM userVM)
        {
            var result = await _mediator.Send(new UpdateUserRequest(userVM.Id, userVM.Username, userVM.Email, userVM.Name, userVM.Surname, userVM.RoleId));
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
                return View(userVM);
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            var result = await _mediator.Send(new DeleteUserRequest(id));
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
                return RedirectToAction("Index");
            }

        }
    }
}
