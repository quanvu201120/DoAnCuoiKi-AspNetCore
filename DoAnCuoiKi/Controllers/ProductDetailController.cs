using DoAnCuoiKi.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DoAnCuoiKi.Controllers
{
    [Authorize]
    public class ProductDetailController : Controller
    {
        private readonly MyDbContext _context;

        public ProductDetailController(MyDbContext context)
        {
            _context = context;
        }

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

        [HttpPost]
        public string HandleAddProductToCart(string productId, int count)
        {
            var product = _context.products.SingleOrDefault(item => item.productId.ToString() == productId);
            var userId = int.Parse(HttpContext.User.Claims.FirstOrDefault(item => item.Type == "userId").Value);

            var cartAdd = new Cart
            {
                name = product.name,
                price = product.price,
                productId = int.Parse(productId),
                userId = userId,
                amount = count,
                
            };
            _context.carts.Add(cartAdd);
            _context.SaveChanges();

            return "true";
        }
    }
}
