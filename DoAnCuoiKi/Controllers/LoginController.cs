using DoAnCuoiKi.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace DoAnCuoiKi.Controllers
{
    public class LoginController : Controller
    {
        private readonly MyDbContext context;

        public LoginController(MyDbContext context)
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
        public async Task<IActionResult> Index(string email, string password)
        {
            var acccount = context.users.SingleOrDefault(item => item.email == email && item.password == password);

            if (acccount != null)
            {

                if(acccount.isDelete == true)
                {
                    ViewBag.loi = "Tài khoản đã bị vô hiệu hóa";
                    return View("Index");
                }

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name,acccount.name),
                    new Claim("userId",acccount.userId.ToString()),
                    new Claim("address",acccount.address.ToString()),
                    new Claim("phone",acccount.phone.ToString()),
                    new Claim("email",acccount.email.ToString()),
                    new Claim(ClaimTypes.Role,acccount.role.ToString())
                };
               
                var claimsIdentity = new ClaimsIdentity(
                    claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var claimPrincipal = new ClaimsPrincipal(claimsIdentity);
                await HttpContext.SignInAsync(claimPrincipal);

                return RedirectToAction("Index","Home");
            }
            else
            {
                ViewBag.loi = "Tài khoản mật khẩu không chính xác";
                return View("Index");
            }


        }


        [HttpGet("/Logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Login");
        }



    }
}
