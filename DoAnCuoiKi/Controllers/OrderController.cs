using DoAnCuoiKi.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DoAnCuoiKi.Controllers
{

    [Authorize]
    public class OrderController : Controller
    {
        private readonly MyDbContext _context;

        public OrderController(MyDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var userId = HttpContext.User.Claims.FirstOrDefault(item => item.Type == "userId").Value;

            var myOrders = await _context.orders.Where(item => item.userId.ToString() == userId).OrderByDescending(item => item.dateCreate).ToListAsync();
            
            return View(myOrders);
        }


        [HttpPost]
        public string HandleClickOrder(string orderId)
        {

            return "/OrderDetail/Index?orderId=" + orderId ;
        }
    }
}
