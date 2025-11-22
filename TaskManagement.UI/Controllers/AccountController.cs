using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.WebUtilities;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Application.Dtos;
using TaskManagement.Application.Requests;
using TaskManagement.UI.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TaskManagement.UI.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
	{
		private readonly IMediator _mediator;
		

		public AccountController(IMediator mediator)
		{
			_mediator = mediator;
		}

		public IActionResult Login()
		{
			return View( new LoginRequest("",""));
		}
		[HttpPost]
		public async Task<IActionResult> LoginAsync(LoginRequest request)
		{ 
			var result  =await this._mediator.Send(request);
			if (result.isSuccess)
			{
				if (result.data != null)
				{
					await SetAuthCookie(result.data,request.RememberMe);
                    return RedirectToAction("Index", "Home", new { area = result.data.Role.ToString() });
                }
				else
				{
                    ModelState.AddModelError("Login failed.", "Role failed");
                }

					return RedirectToAction("Index", "Home", new { area = result.data.Role.ToString() });
                //return RedirectToAction("Index", "Home", new {area="Admin"});
			}
			else
			{
				// Handle errors, e.g., show error message
				if (result.Errors!=null && result.Errors.Count>0)
				{
					foreach (var error in result.Errors)
					{
						ModelState.AddModelError(error.PropertyName, error.ErrorMessage ?? "Login failed.");  
					}
				}
				else
				{
					ModelState.AddModelError(string.Empty, result.ErrorMessage ?? "An error occurred during login.");
				}
				return View(request);
			}
			
		}
		public IActionResult Register()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> RegisterAsync(RegisterRequest request)
		{
			 var result = await _mediator.Send(request);
			if (result.isSuccess)
			{
				return RedirectToAction("Login", "Account");
			}
			else
			{ 				// Handle errors, e.g., show error message
				if (result.Errors != null && result.Errors.Count > 0)
				{
					foreach (var error in result.Errors)
					{
						ModelState.AddModelError(error.PropertyName, error.ErrorMessage ?? "Registration failed.");
					}
				}
				else
				{
					ModelState.AddModelError(string.Empty, result.ErrorMessage ?? "An error occurred during registration.");
				}
				return View(request);
			}

		}
		public async Task<IActionResult> Logout()
		{
			await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
			return RedirectToAction("Login", "Account");
		}

        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword(string token)
		{

			return View( new ResetPasswordVM { Token=token});
        }
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordVM vm)
		{
			var realToken= Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(vm.Token));
            var result = await _mediator.Send(new ResetPasswordRequest(realToken, vm.NewPassword));
			if (result.isSuccess)
			{
				return RedirectToAction("Login", "Account");
			}
			else
			{
				// Handle errors, e.g., show error message
				if (result.Errors != null && result.Errors.Count > 0)
				{
					foreach (var error in result.Errors)
					{
						ModelState.AddModelError(error.PropertyName, error.ErrorMessage ?? "Password reset failed.");
					}
				}
				else
				{
					ModelState.AddModelError(string.Empty, result.ErrorMessage ?? "An error occurred during password reset.");
				}
			}
                return View(vm);
        }


		






        private async Task  SetAuthCookie(LoginResponseDto? dto,bool rememberMe)
		{
			User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Name);
			var claims = new List<Claim>
		{   new Claim("UserId", dto.UserId.ToString()),
            new Claim(ClaimTypes.Name, dto.Name),
			new Claim("FullName", dto.Name+" "+dto.Surname),
			new Claim(ClaimTypes.Role, dto.Role.ToString()),
		};

			var claimsIdentity = new ClaimsIdentity(
				claims, CookieAuthenticationDefaults.AuthenticationScheme);

			var authProperties = new AuthenticationProperties
			{
				//AllowRefresh = <bool>,
				// Refreshing the authentication session should be allowed.

				ExpiresUtc = DateTimeOffset.UtcNow.AddDays(30),
				// The time at which the authentication ticket expires. A 
				// value set here overrides the ExpireTimeSpan option of 
				// CookieAuthenticationOptions set with AddCookie.

				IsPersistent = rememberMe,//Beni hatıala alanının yönetimi
				// Whether the authentication session is persisted across 
				// multiple requests. When used with cookies, controls
				// whether the cookie's lifetime is absolute (matching the
				// lifetime of the authentication ticket) or session-based.

				//IssuedUtc = <DateTimeOffset>,
				// The time at which the authentication ticket was issued.

				//RedirectUri = <string>
				// The full path or absolute URI to be used as an http 
				// redirect response value.
			};

			await HttpContext.SignInAsync(
				CookieAuthenticationDefaults.AuthenticationScheme,
				new ClaimsPrincipal(claimsIdentity),
				authProperties);
		}

	}
}
