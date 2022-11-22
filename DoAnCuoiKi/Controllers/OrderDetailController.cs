using DoAnCuoiKi.Data;
using DoAnCuoiKi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DoAnCuoiKi.Controllers
{
    [Authorize]
    public class OrderDetailController : Controller
    {
        private readonly MyDbContext _context;

        public OrderDetailController(MyDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            string orderId = HttpContext.Request.Query["orderId"];

            var orderDetail = await _context.OrderDetails.Where(item => item.orderId.ToString() == orderId).ToListAsync();
            var order = await _context.orders.FirstOrDefaultAsync(item => item.orderId.ToString() == orderId);

            var data = new OrderDetailModelcs
            {
                order = order,
                orderDetails = orderDetail
            };

            return View(data);
        }
    }
}
