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
    public class BrandsController : Controller
    {
        private readonly MyDbContext _context;

        public BrandsController(MyDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Brands
        public async Task<IActionResult> Index()
        {
            var data = await _context.brands
                .Where(item => item.isDelete == false)
                .ToListAsync();
            return View(data);
        }

        // GET: Admin/Brands/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.brands == null)
            {
                return NotFound();
            }

            var brand = await _context.brands
                .FirstOrDefaultAsync(m => m.brandId == id);
            if (brand == null)
            {
                return NotFound();
            }

            return View(brand);
        }

        // GET: Admin/Brands/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Brands/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("brandId,name")] Brand brand)
        {
            if (ModelState.IsValid)
            {
                _context.Add(brand);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(brand);
        }

        // GET: Admin/Brands/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.brands == null)
            {
                return NotFound();
            }

            var brand = await _context.brands.FindAsync(id);
            if (brand == null)
            {
                return NotFound();
            }
            return View(brand);
        }

        // POST: Admin/Brands/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, string name)
        {
            var brand = await _context.brands.FirstOrDefaultAsync(item => item.brandId == id);
            if (brand == null)
            {
                return View();
            }

            brand.name = name;
            _context.brands.Update(brand);
            await _context.SaveChangesAsync();

            return RedirectToAction("index");
        }

        // GET: Admin/Brands/Delete/5
        public async Task<IActionResult> Delete(int? id)
        { 
            if (id == null || _context.brands == null)
            {
                return NotFound();
            }

            var brand = await _context.brands
                .FirstOrDefaultAsync(m => m.brandId == id);
            if (brand == null)
            {
                return NotFound();
            }

            return View(brand);
        }

        // POST: Admin/Brands/Delete/5

        /*
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.brands == null)
            {
                return Problem("Entity set 'MyDbContext.brands'  is null.");
            }
            var brand = await _context.brands.FindAsync(id);
            if (brand != null)
            {
                _context.brands.Remove(brand);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        */

        [HttpPost]
        public async Task<Boolean> HandleDeleteBrandAdmin(int id)
        {

            var brand = await _context.brands.FirstOrDefaultAsync(item => item.brandId == id);

            if (brand == null)
            {
                return false;
            }

            brand.isDelete = true;
            _context.brands.Update(brand);
            await _context.SaveChangesAsync();

            return true;
        }

        private bool BrandExists(int id)
        {
          return _context.brands.Any(e => e.brandId == id);
        }

        public async Task<IActionResult> Restore()
        {
            var brands = await _context.brands.Where(item => item.isDelete == true).ToListAsync();

            return View(brands);
        }
        [HttpPost]
        public async Task<Boolean> HandleRestore(int id)
        {
            if (id == null)
            {
                return false;
            }

            var brand = await _context.brands.FirstOrDefaultAsync(item => item.brandId == id);

            if (brand == null) { return false; }

            brand.isDelete = false;

            _context.brands.Update(brand);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
