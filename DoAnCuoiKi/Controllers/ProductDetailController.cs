using DoAnCuoiKi.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DoAnCuoiKi.Controllers
{
    public class ProductDetailController : Controller
    {
        private readonly MyDbContext _context;

        public ProductDetailController(MyDbContext context)
        {
            _context = context;
        }

        [Authorize]
        public IActionResult Index()
        {
            //params url
            string productIdParams = HttpContext.Request.Query["productId"];

            var product = _context.products.SingleOrDefault(item => item.productId.ToString() == productIdParams);

            if(product == null)
            {
                return RedirectToAction("Index","NotFoundCustomer");
            }

            return View(product);
        }
    }
}
