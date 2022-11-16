using Microsoft.AspNetCore.Mvc;

namespace DoAnCuoiKi.Controllers
{
    public class NotFoundCustomerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
