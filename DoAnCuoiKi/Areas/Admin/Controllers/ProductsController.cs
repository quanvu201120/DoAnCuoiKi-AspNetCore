using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DoAnCuoiKi.Data;
using Microsoft.AspNetCore.Authorization;
using DoAnCuoiKi.Areas.Admin.Models;
using Microsoft.IdentityModel.Abstractions;

namespace DoAnCuoiKi.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ProductsController : Controller
    {
        private readonly MyDbContext _context;

        public ProductsController(MyDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Products
        public async Task<IActionResult> Index()
        {

            var data = await _context.products
                .Where(item => item.isDelete == false)
                .Join(_context.brands, p => p.brandId, b => b.brandId, (p, b) => new { product = p, brandName = b.name })
                .Join(_context.categories, p => p.product.categoryId, c => c.categoryId, (p, c) => new ProductAdminModel { product = p.product, brandName = p.brandName, categoryName = c.name })
                .ToListAsync();
            
            return View(data);
        }

        // GET: Admin/Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.products == null)
            {
                return NotFound();
            }

            var product = await _context.products
                .Include(p => p.brand)
                .Include(p => p.category)
                .FirstOrDefaultAsync(m => m.productId == id);
            if (product == null)
            {
                return NotFound();
            }

            var brand = await _context.brands.FirstOrDefaultAsync(item => item.brandId == product.brandId);
            var categoriy = await _context.categories.FirstOrDefaultAsync(item => item.categoryId == product.categoryId);

            ViewBag.brand = brand.name;
            ViewBag.categoriy = categoriy.name;

            return View(product);
        }

        // GET: Admin/Products/Create
        public IActionResult Create()
        {
            var brands = _context.brands.ToList();
            var categories = _context.categories.ToList();

            var data = new ProductCreateDataModel { brand = brands, category = categories };

            return View(data);
        }

        // POST: Admin/Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string name, string description, IFormFile image, int amount, int price, int brandId, int categoryId)
        {
            string[] fileName = image.FileName.Split('.');
            var type = fileName[1];
            var imageName = Guid.NewGuid() + "." + type;

            var fullPath = Path.Combine(Directory.GetCurrentDirectory(), "ProductImage", imageName);


            using (var myFile = new FileStream(fullPath, FileMode.Create))
            {
                image.CopyToAsync(myFile);
            }

            var product = new Product
            {
                name = name,
                description = description,
                amount = amount,
                image = imageName,
                price = price,
                brandId = brandId,
                categoryId = categoryId,
            };

            _context.products.Add(product);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        // GET: Admin/Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.products == null)
            {
                return NotFound();
            }

            var product = await _context.products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            var brands = _context.brands.ToList();
            var categories = _context.categories.ToList();

            var data = new ProductEditDataModel
            {
                product = product,
                brands = brands,
                categories = categories,
            };

            return View(data);
        }

        // POST: Admin/Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, string name, string description, IFormFile ? image, int amount, int price, int brandId, int categoryId)
        {

            var product = await _context.products.FirstOrDefaultAsync(item => item.productId == id);
                
            if (product == null)
            {
                return View();
            }
                
            if(image != null)
            {

                var path_of_image_pre = Path.Combine(Directory.GetCurrentDirectory(), "ProductImage", product.image);
                System.IO.File.Delete(path_of_image_pre);



                string[] fileName = image.FileName.Split('.');
                var type = fileName[1];
                var newImageName = Guid.NewGuid() + "." + type;

                var fullPathNewImage = Path.Combine(Directory.GetCurrentDirectory(), "ProductImage", newImageName);

                using (var myFile = new FileStream(fullPathNewImage, FileMode.Create))
                {
                    image.CopyToAsync(myFile);
                }

                product.image = newImageName;
            }


            product.name = name;
            product.description = description;
            product.amount = amount;
            product.price = price;
            product.brandId = brandId;
            product.categoryId = categoryId;

            _context.products.Update(product);
            await _context.SaveChangesAsync();

            return RedirectToAction("index");
        }

        [HttpPost]
        public async Task<Boolean> HandleDeleteProductAdmin(int id)
        {

            var product = await _context.products.FirstOrDefaultAsync(item => item.productId == id);

            if(product == null)
            {
                return false;
            }

            product.isDelete = true;
            _context.products.Update(product);
            await _context.SaveChangesAsync();

            return true;
        }

        private bool ProductExists(int? id)
        {
          return _context.products.Any(e => e.productId == id);
        }


        public async Task<IActionResult> Restore()
        {
            var products = await _context.products.Where(item => item.isDelete == true).ToListAsync();

            return View(products);
        }
        [HttpPost]
        public async Task<Boolean> HandleRestore(int id)
        {
            if(id == null)
            {
                return false;
            }

            var product = await _context.products.FirstOrDefaultAsync(item => item.productId == id);




            if(product == null) { return false; }



            var carts = await _context.carts.Where(item => item.productId == product.productId).ToListAsync();
            _context.carts.RemoveRange(carts);
            await _context.SaveChangesAsync();

            product.isDelete = false;
            _context.products.Update(product);
            await _context.SaveChangesAsync();

            return true;
        }


    }
}
