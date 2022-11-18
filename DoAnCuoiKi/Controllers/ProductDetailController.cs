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

            var cartExits = _context.carts.SingleOrDefault(item => item.userId == userId && item.productId == product.productId); ;

            if(cartExits == null)
            {
                var cartAdd = new Cart
                {
                    name = product.name,
                    price = product.price,
                    productId = int.Parse(productId),
                    userId = userId,
                    amount = count,
                    image = product.image,

                };
                _context.carts.Add(cartAdd);
            }
            else
            {
                cartExits.amount += count;

                if(cartExits.amount > product.amount)
                {
                    return "false";

                }
                _context.carts.Update(cartExits);
            }

            _context.SaveChanges();
            return "true";
        }
    }
}
