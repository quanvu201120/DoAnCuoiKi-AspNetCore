using Microsoft.AspNetCore.Mvc;

namespace DoAnCuoiKi.Controllers
{
    public class ContactsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
