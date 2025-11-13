using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TaskManagement.UI.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles ="Admin")]
	public class HomeController : Controller
	{
		[Area("Admin")]
		public IActionResult Index()
		{
			
			return View();
		}
	}
}
