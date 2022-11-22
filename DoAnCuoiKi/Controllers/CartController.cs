using DoAnCuoiKi.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            var name = HttpContext.User.Identity.Name;
            var address = HttpContext.User.Claims.FirstOrDefault(item => item.Type == "address").Value;
            var phone = HttpContext.User.Claims.FirstOrDefault(item => item.Type == "phone").Value;
            var email = HttpContext.User.Claims.FirstOrDefault(item => item.Type == "email").Value;

            var myCarts = myDbContext.carts.Where(item => item.userId.ToString() == userId).ToList();

            double tongSanPham = 0;
            double tongTien = 0;
            double phiVanChuyen = 0;

            myCarts.ForEach(item => { tongSanPham = tongSanPham + item.price * item.amount; });

            if(tongSanPham > 0)
            {
                tongTien = tongSanPham + 50000;
                phiVanChuyen = 50000;
            }


            ViewBag.name = name ;
            ViewBag.address = address ;
            ViewBag.phone = phone ;
            ViewBag.email = email;

            ViewBag.tongSanPham = tongSanPham;
            ViewBag.phiVanChuyen = phiVanChuyen;
            ViewBag.tongTien = tongTien;

            return View(myCarts);
        }

        [HttpPost]
        public async Task<IActionResult> HanleSubmitCart([Bind("customerName,customerAddress ,customerEmail,customerPhone ,otherInformation ,totalPrice")] Order order)
        {
            order.dateCreate = DateTime.Now;
            var userId = HttpContext.User.Claims.FirstOrDefault(item => item.Type == "userId").Value;

            var myCarts = await myDbContext.carts.Where(item => item.userId.ToString() == userId).ToListAsync();

            order.userId = int.Parse(userId);

            myDbContext.orders.Add(order);
            await myDbContext.SaveChangesAsync();

            var orderId = order.orderId;

            myCarts.ForEach(async item => {

                var productUpdate =  myDbContext.products.FirstOrDefault(product => product.productId == item.productId);

                productUpdate.amount -= item.amount;
                myDbContext.products.Update(productUpdate);

                var orderDetails = new OrderDetails
                {
                    productName = item.name,
                    productPrice = item.price,
                    image = item.image,
                    productAmout = item.amount,
                    totalPrice = item.amount * item.price,
                    orderId = (int)orderId,
                };
                myDbContext.OrderDetails.Add(orderDetails);
                 myDbContext.SaveChanges();
            });

            myDbContext.carts.RemoveRange(myCarts);

            await myDbContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<string> HanleChangeCount(char action, int idCart)
        {

            var cart = await myDbContext.carts.FirstOrDefaultAsync(item => item.cartId == idCart);

            var product = await myDbContext.products.FirstOrDefaultAsync(item => item.productId == cart.productId);

            if (action == '+')
            {
                if ((cart.amount + 1) > product.amount) { return "false"; }
                cart.amount += 1;
                myDbContext.carts.Update(cart);
                await myDbContext.SaveChangesAsync();
                return "true";
            }
            else
            {

                cart.amount -= 1;
                if (cart.amount == 0)
                {
                    myDbContext.carts.Remove(cart);
                }
                else
                {
                    myDbContext.carts.Update(cart);
                }

                await myDbContext.SaveChangesAsync();
                return "true";

            }


        }



        [HttpPost]
        public async Task<string> HanleDeleteCart(int idCart)
        {

            var cart = await myDbContext.carts.FirstOrDefaultAsync(item => item.cartId == idCart);

            if(cart == null) { return "false"; }

            myDbContext.carts.Remove(cart);
            await myDbContext.SaveChangesAsync();

            return "true";

        }
    }
}
