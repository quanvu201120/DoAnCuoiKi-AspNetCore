using Microsoft.AspNetCore.Mvc;

namespace DoAnCuoiKi.Controllers
{
    public class AboutUsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
