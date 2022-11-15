using Microsoft.AspNetCore.Mvc;

namespace DoAnCuoiKi.Controllers
{
	public class RegisterController : Controller
	{
		public IActionResult Index()
		{
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }


	}
}
