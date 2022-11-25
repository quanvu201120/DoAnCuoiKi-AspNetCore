using DoAnCuoiKi.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DoAnCuoiKi.Controllers
{
    [Authorize]
    public class UserInfoController : Controller
    {
        private readonly MyDbContext _context;

        public UserInfoController(MyDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var id = HttpContext.User.Claims.FirstOrDefault(it => it.Type == "userId").Value;

            var user =  await _context.users.FirstOrDefaultAsync(item => item.userId.ToString() == id);

            if (user == null) return RedirectToAction("Index", "NotFoundCustomer");

            return View(user);
        }

        [HttpPost]
        public async Task<Boolean> HandleChangePassword(string currentPass, string newPass)
        {

            var id = HttpContext.User.Claims.FirstOrDefault(it => it.Type == "userId").Value;

            var user = await _context.users.FirstOrDefaultAsync(item => item.userId.ToString() == id);

            if(currentPass != user.password) { return false; }

            user.password = newPass;
            _context.users.Update(user);
            await _context.SaveChangesAsync();

            return true;
        }


        [HttpPost]
        public async Task<IActionResult> HanleUpdateUserInfo(string name, string gender, string phone, string email, string address)
        {

            var id = HttpContext.User.Claims.FirstOrDefault(it => it.Type == "userId").Value;

            var user = await _context.users.FirstOrDefaultAsync(item => item.userId.ToString() == id);

            if (ModelState.IsValid)
            {
                user.name = name;
                user.gender = gender;
                user.phone = phone;
                user.email = email;
                user.address = address;
                _context.users.Update(user);
                await _context.SaveChangesAsync();
            }

            return Redirect("Index");
        }
            
    }
}
