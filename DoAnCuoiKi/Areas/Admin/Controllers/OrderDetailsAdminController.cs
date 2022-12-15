using DoAnCuoiKi.Data;
using DoAnCuoiKi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DoAnCuoiKi.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class OrderDetailsAdminController : Controller
    {


        private readonly MyDbContext _context;

        public OrderDetailsAdminController(MyDbContext context)
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

        [HttpPost]
        public async Task<Boolean> UpdateStatusOrder(int orderId, int status)
        {

            var order = await _context.orders.FirstOrDefaultAsync(item => item.orderId == orderId);

            if (order == null) { return false; }

            if(status == 2)
            {
                order.dateReceive = DateTime.Now;
            }

            order.status = status;
            _context.orders.Update(order);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
