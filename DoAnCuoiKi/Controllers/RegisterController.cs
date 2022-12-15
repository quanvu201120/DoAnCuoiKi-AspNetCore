using DoAnCuoiKi.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DoAnCuoiKi.Controllers
{
	public class RegisterController : Controller
	{
        private readonly MyDbContext context;

        public RegisterController(MyDbContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
		{
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([Bind("userId,name,phone,email,gender,password,address")] User user)
        {
            user.role = "Customer";
            user.isDelete = false;
            var check = context.users.SingleOrDefault(item => item.email.ToUpper() == user.email.ToUpper());

            if (ModelState.IsValid && check == null)
            {
                context.Add(user);
                await context.SaveChangesAsync();
                return RedirectToAction("Index","Login");
            }
            return View("Index");
        }




    }
}
