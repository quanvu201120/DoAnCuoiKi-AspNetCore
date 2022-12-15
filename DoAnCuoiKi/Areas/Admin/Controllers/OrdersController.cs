using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DoAnCuoiKi.Data;
using Microsoft.AspNetCore.Authorization;

namespace DoAnCuoiKi.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class OrdersController : Controller
    {
        private readonly MyDbContext _context;

        public OrdersController(MyDbContext context)
        {
            _context = context;
        }





        // GET: Admin/Orders
        public async Task<IActionResult> Index()
        {
            var myDbContext = _context.orders.Include(o => o.user);
            return View(await myDbContext.ToListAsync());
        }

        [HttpPost]
        public string HandleClickOrderAdmin(string orderId)
        {
            return "OrderDetailsAdmin/Index?orderId=" + orderId;
        }


    }
}
