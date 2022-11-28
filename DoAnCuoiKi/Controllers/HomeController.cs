using DoAnCuoiKi.Data;
using DoAnCuoiKi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace DoAnCuoiKi.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly MyDbContext _context;

        public HomeController(MyDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            //params url
            string categoryId = HttpContext.Request.Query["categoryId"];
            string brandId = HttpContext.Request.Query["brandId"];
            string price = HttpContext.Request.Query["price"];
            string search = HttpContext.Request.Query["search"];


            //viewbag content
            var products = _context.products.Where(item => item.isDelete == false).ToList();
            List<Brand> brands = new List<Brand>();
            var cateName = "";

            ViewBag.search = search;

            //Mặc định category 1
            if (categoryId.IsNullOrEmpty())
            {
                categoryId = "1";
                cateName = _context.categories.FirstOrDefault(item => item.categoryId == 1).name;
                products = products.Where(item => item.categoryId == 1).ToList();
            }


            if (!search.IsNullOrEmpty())
            {
                products = products.Where(_ => _.name.Contains(search)).ToList();            
            }

            //filter theo cate id
            if (!categoryId.IsNullOrEmpty())
            {
                cateName = _context.categories.FirstOrDefault(item => item.categoryId.ToString() == categoryId).name;
                products = products.Where(item => item.categoryId.ToString() == categoryId).ToList();
            }

            //Lấy tất cả brand có trong cate
            var listIdBrand = products.GroupBy(x => x.brandId).Select(group => group.First().brandId);//unique id brand
            foreach(var item in listIdBrand)
            {
                var tmp = _context.brands.FirstOrDefault(i => i.brandId == item);
                brands.Add(tmp);
            }


            if (brandId.IsNullOrEmpty() || brandId == "0"){
                //if (categoryId.IsNullOrEmpty())
                //{
                //    products = products.Where(item => item.categoryId == 1).ToList();
                //}
                //else
                //{
                //    products = products.Where(item => item.categoryId.ToString() == categoryId).ToList();
                //}
            }
            else
            {
                products = products.Where(item => item.brandId.ToString() == brandId).ToList();
            }

            //sort price
            if( price.IsNullOrEmpty() ||price == "asc")
            {
                price = "asc";
                products = products.OrderBy(x => x.price).ToList();
            }
            else {
                products = products.OrderByDescending(x => x.price).ToList();
            }


            var data = new ProductDataModel
            {
                categoryId = categoryId,
                brandId = brandId,
                price = price,

                categoryName = cateName,
                products = products,
                brands = brands
            };

            return View("Index", data);
        }


        //Nhận id từ ajax trả về đường dẫn
        //=> sau đó bên ajax success redirect tới đường dẫn
        //=> action index lấy id trên đường dẫn và xử lí
        [HttpPost]
        public string HanleClickFilter(string categoryId, string? brandId = "", string ? price = "", string ? search = "" )
        {
            return "/Home/Index?categoryId=" + categoryId + "&brandId=" + brandId + "&price=" + price+ "&search=" + search;
        }

        //Nhận id từ ajax trả về đường dẫn
        //=> sau đó bên ajax success redirect tới đường dẫn
        //=> action index lấy id trên đường dẫn và xử lí
        [HttpPost]
        public string HandleClickProduct(string productId)
        {
            return "/ProductDetail/Index?productId=" + productId;
        }
    }
}
