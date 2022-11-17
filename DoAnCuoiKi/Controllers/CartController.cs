using DoAnCuoiKi.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DoAnCuoiKi.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly MyDbContext myDbContext;

        public CartController(MyDbContext myDbContext)
        {
            this.myDbContext = myDbContext;
        }

        public IActionResult Index()
        {
            var userId = HttpContext.User.Claims.FirstOrDefault(item => item.Type == "userId").Value;

            var myCarts = myDbContext.carts.Where(item => item.userId.ToString() == userId).ToList();

            return View(myCarts);
        }

        [HttpPost]
        public async Task<IActionResult> HanleSubmitCart([Bind("customerName,customerAddress ,customerEmail,customerPhone ,otherInformation ,totalPrice")] Order order)
        {
            order.dateCreate = DateTime.Now;
            var userId = HttpContext.User.Claims.FirstOrDefault(item => item.Type == "userId").Value;

            order.userId = int.Parse(userId);

            myDbContext.orders.Add(order);
            await myDbContext.SaveChangesAsync();

            var orderId = order.orderId;

            return RedirectToAction("Index");
        }
    }
}
