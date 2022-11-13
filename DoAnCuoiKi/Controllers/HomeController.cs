using Microsoft.AspNetCore.Mvc;

namespace DoAnCuoiKi.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
